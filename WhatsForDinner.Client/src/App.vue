<script setup lang="ts">
import Header from './components/App-header.vue'
import CRUDDishMenu from '@/components/CRUDDishMenu.vue'
import { computed, onMounted, provide, reactive, ref } from 'vue'
import Footer from '@/components/App-footer.vue'
import axios from 'axios'
import { TagType } from '@/TypeScriptFiles/TagTypes'
import { IDish, ITag } from '@/TypeScriptFiles/Interfaces'
import { TypeImage } from '@/TypeScriptFiles/Types'

const standardImage: TypeImage = {
  title: 'standard_dish_image',
  content: '/dishes/pure-kotleta.jpg'
}

const dataLoaded = ref<boolean>(false)
const BaseDishesCollection = reactive<IDish[]>([])
const currentDishForChanges = ref<IDish>({
  dishId: '00000000-0000-0000-0000-000000000000',
  title: '',
  tags: [],
  description: '',
  img: standardImage
})

const isCRUDDishMenuOpen = ref<boolean>(false)

// Сортировка тегов по типам
const ingredientTags = computed<ITag[]>((): ITag[] => {
  const tags: ITag[] = []
  BaseDishesCollection.forEach((dish): void => {
    dish.tags.forEach((tag): void => {
      if (tag.type === TagType.Ingredient && !tags.some((t): boolean => t.title === tag.title)) {
        tags.push({ ...tag, state: 0 })
      }
    })
  })
  tags.sort(compareLettersForSort)
  return createReactiveTags(tags)
})

const typeTags = computed<ITag[]>((): ITag[] => {
  const tags: ITag[] = []
  BaseDishesCollection.forEach((dish: IDish): void => {
    dish.tags.forEach((tag: ITag): void => {
      if (tag.type === TagType.Type && !tags.some((t): boolean => t.title === tag.title)) {
        tags.push({ ...tag, state: 0 })
      }
    })
  })
  tags.sort(compareLettersForSort)
  return createReactiveTags(tags)
})

const reasonTags = computed<ITag[]>((): ITag[] => {
  const tags: ITag[] = []
  BaseDishesCollection.forEach((dish): void => {
    dish.tags.forEach((tag): void => {
      if (tag.type === TagType.Reason && !tags.some((t): boolean => t.title === tag.title)) {
        tags.push({ ...tag, state: 0 })
      }
    })
  })
  tags.sort(compareLettersForSort)
  return createReactiveTags(tags)
})

onMounted(async () => {
  await fetchBaseDishesCollectionData()
})

async function fetchBaseDishesCollectionData(): Promise<void> {
  BaseDishesCollection.length = 0
  await axios
    .get('http://localhost:5262/api/dishes/getAll')
    .then((response) =>
      response.data.forEach((dish: IDish): void => {
        BaseDishesCollection.push({
          dishId: dish.dishId,
          title: dish.title,
          description: dish.description ?? 'Описание отсутствует',
          tags: dish.tags ?? [],
          img:
            dish.img === null
              ? standardImage
              : {
                  title: dish.img.title,
                  content: `data:image/jpg;base64,${dish.img.content}`
                }
        })
      })
    )
    .then((response) => {
      console.log(response)
    })
    .then(() => {
      dataLoaded.value = true
    })
    .catch((error) => {
      console.error(error)
    })
}

async function createDish(): Promise<void> {
  await axios
    .post('http://localhost:5262/api/dishes/createDish', currentDishForChanges.value)
    .then((response) => {
      BaseDishesCollection.push({ ...response.data })
    })
    .then((response) => {
      console.log(response)
    })
    .catch((error) => {
      console.error(error)
    })
  closeOrOpenCRUDDishMenu()
}

async function updateDish(): Promise<void> {
  await axios
    .put('http://localhost:5262/api/dishes/updateDish', currentDishForChanges.value)
    .then((response) => {
      if (response.status === 200) {
        BaseDishesCollection.map((dish: IDish): IDish => {
          const newDish: IDish = response.data
          if (newDish.dishId === dish.dishId) {
            dish = newDish
          }
          return dish
        })
      }
    })
    .then((response) => {
      console.log(response)
    })
    .catch((error) => {
      console.error(error)
    })
  closeOrOpenCRUDDishMenu()
}

async function deleteDish(id: string): Promise<void> {
  await axios
    .delete('http://localhost:5262/api/dishes/deleteDish', {
      params: { id }
    })
    .then((response) => {
      if (response.status === 200) {
        const index = BaseDishesCollection.findIndex((dish) => dish.dishId === id)
        if (index !== -1) {
          BaseDishesCollection.splice(index, 1)
        }
      }
    })
    .then((response) => {
      console.log(response)
    })
    .catch((error) => {
      console.error(error)
    })
}

function compareLettersForSort(a: ITag, b: ITag): number {
  // Получаем строки для сравнения
  const wordA = a.title.toLowerCase()
  const wordB = b.title.toLowerCase()

  // Сравниваем строки посимвольно
  for (let i = 0; i < Math.min(wordA.length, wordB.length); i++) {
    const charA = wordA.charCodeAt(i)
    const charB = wordB.charCodeAt(i)

    // Если символы не равны, возвращаем результат сравнения
    if (charA !== charB) return charA - charB
  }

  // Если строки равны до конца, возвращаем 0
  return wordA.length - wordB.length
}

function createReactiveTags(tags: ITag[]): ITag[] {
  return tags.map((tag) => reactive<ITag>(tag))
}

function createUrlForImage(image: TypeImage): string {
  // Преобразуем строку в массив байтов
  const bytes: number[] = hexToBytes(image.content)

  // Создаём Blob-объект из массива байтов
  const blob = new Blob([new Uint8Array(bytes)], { type: 'image/jpg' })

  return URL.createObjectURL(blob).substring(5)

  /*  const base64String = btoa(String.fromCharCode(...new Uint8Array(bytes)))
  return `data:image/png;base64,${base64String}`*/
}

function hexToBytes(hex: string): number[] {
  if (!hex.startsWith('0x')) return []
  hex = hex.slice(2) // Убираем префикс '0x'
  const result: number[] = []
  for (let i = 0; i < hex.length; i += 2) {
    result.push(parseInt(hex.substring(i, i + 2), 16))
  }
  return result
}

function closeOrOpenCRUDDishMenu(): void {
  const state = isCRUDDishMenuOpen.value
  if (state) clearCurrentDishForChanges()
  isCRUDDishMenuOpen.value = !state
}

function clearCurrentDishForChanges(): void {
  currentDishForChanges.value.title = ''
  currentDishForChanges.value.tags = []
  currentDishForChanges.value.description = ''
  currentDishForChanges.value.img = standardImage
}

provide('dishes', {
  dishes: BaseDishesCollection,
  currentDishForChanges,
  standardImage,
  ingredientTags,
  typeTags,
  reasonTags
})

provide('CRUDForDishes', {
  createDish,
  deleteDish,
  updateDish
})

provide('dialogMenus', {
  closeOrOpenCRUDDishMenu
})
provide('dataLoaded', {
  dataLoaded
})
</script>

<template>
  <div>
    <c-r-u-d-dish-menu
      v-if="isCRUDDishMenuOpen"
      @close-or-open-c-r-u-d-dish-menu="closeOrOpenCRUDDishMenu"
    />
    <Header />
    <div class="m-auto mb-20 mt-14 h-screen w-4/5">
      <main>
        <RouterView />
      </main>
    </div>
    <Footer />
  </div>
</template>

<style scoped></style>
