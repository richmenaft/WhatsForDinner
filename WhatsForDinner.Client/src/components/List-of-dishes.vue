<script setup lang="ts">
import { inject } from 'vue'
import DishCard from '@/components/Dish-card.vue'
import { IDish } from '@/TypeScriptFiles/Interfaces'

interface DefinedProps {
  dishes: IDish[]
}

interface InjectedDialogMenusProps {
  closeOrOpenCRUDDishMenu: () => void
}

const { dishes } = defineProps<DefinedProps>()

const { closeOrOpenCRUDDishMenu } = inject('dialogMenus') as InjectedDialogMenusProps
</script>

<template>
  <div v-auto-animate class="grid grid-cols-4 gap-5">
    <div
      class="relative cursor-pointer content-center rounded-3xl border border-slate-100 bg-white p-8 transition hover:-translate-y-2 hover:shadow-xl"
      @click="closeOrOpenCRUDDishMenu()"
    >
      <div class="flex justify-center">
        <p
          class="size-fit rounded-full border border-slate-400 px-7 py-3 text-7xl text-slate-500 opacity-50"
        >
          +
        </p>
      </div>
    </div>
    <DishCard
      v-for="dish in dishes"
      :id="dish.dishId"
      :key="dish.dishId"
      :title="dish.title"
      :description="dish.description"
      :tags="dish.tags"
      :img="dish.img"
    />
  </div>
</template>

<style scoped></style>
