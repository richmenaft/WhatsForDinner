<script setup lang="ts">
import RandomDish from '@/components/RandomDish-component.vue'
import Filter from '@/components/Filter-component.vue'
import { inject, onMounted, Reactive, Ref, ref, watch } from 'vue'
import { IDish, ITag, IRandomDish } from '@/TypeScriptFiles/Interfaces.js'
import { TypeImage } from '@/TypeScriptFiles/Types.js'

interface IDishesData {
  dishes: Reactive<IDish[]>
  standardImage: TypeImage
  ingredientTags: Ref<ITag[]>
  typeTags: Ref<ITag[]>
  reasonTags: Ref<ITag[]>
}

interface IDataLoaded {
  dataLoaded: Ref<boolean>
}

const injectedProperties = inject('dishes') as IDishesData
const { dishes, standardImage, ingredientTags, typeTags, reasonTags } = injectedProperties
const { dataLoaded } = inject('dataLoaded') as IDataLoaded

const validTags = ref<ITag[]>([])
const invalidTags = ref<ITag[]>([])

const randomDish = ref<IRandomDish>({
  title: '',
  tags: [],
  description: '',
  img: standardImage
})
const isRandomDishFound = ref<boolean>(false)

onMounted(() => {
  defineValidAndInvalidTags(ingredientTags.value)
  defineValidAndInvalidTags(typeTags.value)
  defineValidAndInvalidTags(reasonTags.value)

  //Если данные в App.vue подгружены с базы данных, то определяем случайное блюдо
  watch(dataLoaded, async (value: boolean): Promise<void> => {
    if (value) {
      await defineRandomDish()
    }
  })
})

function defineValidAndInvalidTags(tags: ITag[]): void {
  for (const tag of tags) {
    switch (tag.state) {
      case 1:
        if (!validTags.value.some((validTag): boolean => validTag.title === tag.title)) {
          if (invalidTags.value.some((invalidTag): boolean => invalidTag.title === tag.title)) {
            const index: number = invalidTags.value.indexOf(tag)
            if (index !== -1) invalidTags.value.splice(index, 1)
          }
          validTags.value.push(tag)
        }
        break
      case -1:
        if (!invalidTags.value.some((invalidTag): boolean => invalidTag.title === tag.title)) {
          if (validTags.value.some((validTag): boolean => validTag.title === tag.title)) {
            const index: number = validTags.value.indexOf(tag)
            if (index !== -1) validTags.value.splice(index, 1)
          }
          invalidTags.value.push(tag)
        }
        break
      case 0:
        if (invalidTags.value.some((invalidTag): boolean => invalidTag.title === tag.title)) {
          const index: number = invalidTags.value.indexOf(tag)
          if (index !== -1) invalidTags.value.splice(index, 1)
        } else if (validTags.value.some((validTag): boolean => validTag.title === tag.title)) {
          const index: number = validTags.value.indexOf(tag)
          if (index !== -1) validTags.value.splice(index, 1)
        }
        break
    }
  }
}

async function defineRandomDish(): Promise<void> {
  for (let i = 0; i < dishes.length; i++) {
    const valuableDishes: IDish[] = dishes.filter(
      (dish: IDish): boolean =>
        (validTags.value.length === 0 ||
          validTags.value.every((tag): boolean =>
            dish.tags.some((dishTag): boolean => dishTag.title === tag.title)
          )) &&
        (invalidTags.value.length === 0 ||
          !invalidTags.value.some((tag): boolean =>
            dish.tags.some((dishTag): boolean => dishTag.title === tag.title)
          ))
    )
    if (valuableDishes.length > 1) {
      let rndDish: IRandomDish
      //Исключаем возможность выпадения одного и того же блюда подряд.
      // eslint-disable-next-line no-constant-condition
      while (true) {
        rndDish = valuableDishes[Math.floor(Math.random() * valuableDishes.length)]
        if (rndDish !== randomDish.value) break
      }
      randomDish.value = rndDish
      isRandomDishFound.value = true
      return
    } else if (valuableDishes.length === 1) {
      randomDish.value = valuableDishes[0]
      isRandomDishFound.value = true
      return
    }
  }
  isRandomDishFound.value = false
}

watch(
  ingredientTags,
  () => {
    defineValidAndInvalidTags(ingredientTags.value)
  },
  { deep: true }
)
watch(
  typeTags,
  () => {
    defineValidAndInvalidTags(typeTags.value)
  },
  { deep: true }
)

watch(
  reasonTags,
  () => {
    defineValidAndInvalidTags(reasonTags.value)
  },
  { deep: true }
)
</script>

<template>
  <div class="flex flex-col gap-y-12">
    <RandomDish
      :title="randomDish?.title"
      :description="randomDish?.description"
      :img="randomDish.img"
      :is-found="isRandomDishFound"
      @define-random-dish="defineRandomDish"
    />
    <hr class="border-1 border-main-font" />
    <Filter />
  </div>
</template>

<style scoped></style>
