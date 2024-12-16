<script setup lang="ts">
import { inject, Reactive, Ref } from 'vue'
import { IDish, ITag } from '@/TypeScriptFiles/Interfaces'
import { TagType } from '@/TypeScriptFiles/TagTypes'
import { TypeImage } from '@/TypeScriptFiles/Types'

interface DefinedProps {
  id: string
  title: string
  description: string
  img: TypeImage
  tags: ITag[]
}

interface IInjectedDishesProps {
  dishes: Reactive<IDish[]>
  currentDishForChanges: Ref<IDish>
}

interface IInjectedDialogMenusProps {
  closeOrOpenCRUDDishMenu: () => void
}

interface IInjectedCRUDForDishesProps {
  deleteDish(id: string): Promise<void>
}

const { id, title, description, img, tags } = defineProps<DefinedProps>()

const { currentDishForChanges } = inject('dishes') as IInjectedDishesProps

const { deleteDish } = inject('CRUDForDishes') as IInjectedCRUDForDishesProps

const { closeOrOpenCRUDDishMenu } = inject('dialogMenus') as IInjectedDialogMenusProps

function defineCurrentDishForChanges(
  title: string,
  description: string,
  img: TypeImage,
  tags: ITag[]
): void {
  currentDishForChanges.value.dishId = id
  currentDishForChanges.value.title = title
  currentDishForChanges.value.description = description
  currentDishForChanges.value.img = img
  currentDishForChanges.value.tags = tags
  closeOrOpenCRUDDishMenu()
}
</script>

<template>
  <div>
    <div
      class="relative cursor-pointer rounded-3xl border border-slate-100 bg-white p-8 transition hover:-translate-y-2 hover:shadow-xl"
    >
      <div class="mb-4 flex justify-end gap-1">
        <img
          src="/src/components/icons/changeDish.png"
          alt="Изменить блюдо"
          class="w-10 rounded-xl border-0 p-2 hover:bg-green-bright"
          @click="defineCurrentDishForChanges(title, description, img, tags)"
        />
        <img
          src="/src/components/icons/deleteDish.png"
          alt="Удалить блюдо"
          class="w-10 rounded-xl border-0 p-2 hover:bg-red-400"
          @click="deleteDish(id)"
        />
      </div>
      <img :src="img.content" alt="Блюдо" class="mb-2 rounded-xl" />
      <h2 class="mb-4 text-center text-xl">{{ title }}</h2>

      <div class="grid grid-cols-3 gap-2">
        <div v-for="(tag, index) in tags" :key="index" class="text-center text-white">
          <div v-if="tag.type === TagType.Ingredient" class="rounded-2xl bg-green-400">
            {{ tag.title }}
          </div>

          <div v-if="tag.type === TagType.Type" class="rounded-2xl bg-orange-500">
            {{ tag.title }}
          </div>

          <div v-if="tag.type === TagType.Reason" class="rounded-2xl bg-blue-500">
            {{ tag.title }}
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped></style>
