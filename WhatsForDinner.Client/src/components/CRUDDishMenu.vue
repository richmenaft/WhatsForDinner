<script setup lang="ts">
import { computed, inject, onActivated, Ref, ref } from 'vue'
import { IDish, ITag } from '@/TypeScriptFiles/Interfaces'
import { TagType } from '@/TypeScriptFiles/TagTypes'
import { TypeImage } from '@/TypeScriptFiles/Types'

interface IInjectedDishesProps {
  currentDishForChanges: Ref<IDish>
}

interface IInjectedCRUDForDishesProps {
  createDish(): Promise<void>
  updateDish(): Promise<void>
}

const { currentDishForChanges } = inject('dishes') as IInjectedDishesProps
const { createDish, updateDish } = inject('CRUDForDishes') as IInjectedCRUDForDishesProps

const emit = defineEmits(['closeOrOpenCRUDDishMenu'])

let isNewDish: boolean = false

const title = ref<string>(currentDishForChanges.value.title)
const description = ref<string>(currentDishForChanges.value.description)
const img = ref<TypeImage>(currentDishForChanges.value.img)
const tags = ref<ITag[]>(currentDishForChanges.value.tags)

let ingredientTimeoutId: number | null = null
let typeTimeoutId: number | null = null
let reasonTimeoutId: number | null = null

const ingredientTags = computed(() => {
  return tags.value
    .filter((tag): boolean => tag.type === TagType.Ingredient)
    .map((tag): string => tag.title)
    .join(',')
})

const typeTags = computed(() => {
  return tags.value
    .filter((tag): boolean => tag.type === TagType.Type)
    .map((tag): string => tag.title)
    .join(',')
})

const reasonTags = computed(() => {
  return tags.value
    .filter((tag): boolean => tag.type === TagType.Reason)
    .map((tag): string => tag.title)
    .join(',')
})

const textAreaIngredient = ref<string>(ingredientTags.value)
const textAreaType = ref<string>(typeTags.value)
const textAreaReason = ref<string>(reasonTags.value)

const isMouseOverImage = ref<boolean>(false)

function showChangeDishIcon(): void {
  isMouseOverImage.value = true
}

function hideChangeDishIcon(): void {
  isMouseOverImage.value = false
}

function debounceOnInputIngredient(event: Event): void {
  if (ingredientTimeoutId !== null) {
    clearTimeout(ingredientTimeoutId)
  }

  ingredientTimeoutId = setTimeout(() => {
    if (event.target instanceof HTMLInputElement) {
      textAreaIngredient.value = event.target.value
    }

    ingredientTimeoutId = null
  }, 500)
}

function debounceOnInputType(event: Event): void {
  if (typeTimeoutId !== null) {
    clearTimeout(typeTimeoutId)
  }

  typeTimeoutId = setTimeout(() => {
    if (event.target instanceof HTMLInputElement) {
      textAreaType.value = event.target.value
    }

    typeTimeoutId = null
  }, 500)
}

function debounceOnInputReason(event: Event): void {
  if (reasonTimeoutId !== null) {
    clearTimeout(reasonTimeoutId)
  }

  reasonTimeoutId = setTimeout(() => {
    if (event.target instanceof HTMLInputElement) {
      textAreaReason.value = event.target.value
    }

    reasonTimeoutId = null
  }, 500)
}

function saveCurrentDishAndProceed(): void {
  currentDishForChanges.value.title = title.value
  currentDishForChanges.value.description = description.value
  currentDishForChanges.value.img = img.value
  textAreaIngredient.value.split(',').forEach((tag): void => {
    currentDishForChanges.value.tags.push({ title: tag, type: TagType.Type })
  })
  textAreaType.value.split(',').forEach((tag): void => {
    currentDishForChanges.value.tags.push({ title: tag, type: TagType.Ingredient })
  })
  textAreaReason.value.split(',').forEach((tag): void => {
    currentDishForChanges.value.tags.push({ title: tag, type: TagType.Reason })
  })
  if (isNewDish) createDish()
  else updateDish()
}

onActivated(() => {
  isNewDish = currentDishForChanges.value.title === '' || currentDishForChanges.value.title === null
})
</script>

<template>
  <section>
    <div class="fixed left-0 top-0 z-10 h-full w-full bg-black opacity-70"></div>

    <div v-auto-animate class="container fixed left-1/4 top-28 z-20 rounded-3xl bg-white p-8">
      <button
        class="absolute right-5 top-5 rounded-full border border-slate-400 bg-red-600 px-4 pb-2 pt-1 text-center text-3xl text-white transition hover:brightness-90"
        @click="emit('closeOrOpenCRUDDishMenu')"
      >
        x
      </button>
      <div class="flex justify-center">
        <label class="block">
          <input
            v-model="title"
            type="text"
            placeholder="Название"
            class="mb-8 block w-full border-0 border-b-2 border-gray-200 px-0.5 text-center text-3xl focus:border-green-bright focus:ring-0"
          />
        </label>
      </div>

      <div class="mb-8 flex flex-row gap-12">
        <div
          class="relative w-1/2 basis-1/2 transition hover:brightness-50"
          @mouseover="showChangeDishIcon()"
          @mouseleave="hideChangeDishIcon()"
        >
          <img
            alt="Изображение блюда"
            :src="img !== null ? img.content : '/dishes/pure-kotleta.png'"
            class="rounded-xl"
          />
          <img
            v-if="isMouseOverImage"
            id="changeDishImageIcon"
            src="/src/components/icons/changeDishImage.png"
            alt="Изменить изображение блюда"
            class="absolute bottom-1/3 right-1/3 w-1/3"
          />
        </div>
        <label class="block basis-1/2">
          <span class="text-gray-700">Описание</span>
          <textarea
            v-model="description"
            type="text"
            placeholder="Очень вкусное блюдо с необычным ароматом"
            rows="10"
            class="mt-1 block w-full resize-none rounded-md border-gray-300 shadow-sm focus:border-indigo-300 focus:ring focus:ring-green-bright focus:ring-opacity-50"
          />
        </label>
      </div>
      <h2 class="mb-2 text-center text-4xl">Теги</h2>
      <div class="mb-4 grid grid-cols-3 gap-8">
        <label class="block">
          <span class="text-gray-700">Ингредиенты</span>
          <textarea
            v-model="textAreaIngredient"
            type="text"
            placeholder="Курица,картошка,молоко,яйца"
            rows="3"
            class="mt-1 block w-full resize-none rounded-md border-gray-300 shadow-sm focus:border-indigo-300 focus:ring focus:ring-green-bright focus:ring-opacity-50"
            @input="debounceOnInputIngredient"
          />
        </label>
        <label class="block">
          <span class="text-gray-700">Тип</span>
          <textarea
            v-model="textAreaType"
            type="text"
            placeholder="Первое,второе,диетическое"
            rows="3"
            class="mt-1 block w-full resize-none rounded-md border-gray-300 shadow-sm focus:border-indigo-300 focus:ring focus:ring-green-bright focus:ring-opacity-50"
            @input="debounceOnInputType"
          />
        </label>
        <label class="block">
          <span class="text-gray-700">Повод</span>
          <textarea
            v-model="textAreaReason"
            type="text"
            placeholder="Праздник,выходной,будни"
            rows="3"
            class="mt-1 block w-full resize-none rounded-md border-gray-300 shadow-sm focus:border-indigo-300 focus:ring focus:ring-green-bright focus:ring-opacity-50"
            @input="debounceOnInputReason"
          />
        </label>
      </div>
      <div class="absolute bottom-7 right-7">
        <button
          class="rounded-full border border-slate-400 bg-green-darker px-4 pb-2 pt-1 text-center text-xl text-white transition hover:brightness-90"
          @click="saveCurrentDishAndProceed"
        >
          Сохранить
        </button>
      </div>
    </div>
  </section>
</template>

<style scoped>
.container {
  width: 50vw;
  height: 75vh;
  margin: 0 auto;
}
</style>
