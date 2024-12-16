<script setup lang="ts">
import { TypeImage } from '@/TypeScriptFiles/Types'

interface DefinedProps {
  title: string
  description: string
  img: TypeImage
  isFound: boolean
}

const { title, description, img, isFound } = defineProps<DefinedProps>()

const emit = defineEmits(['defineRandomDish'])
</script>

<template>
  <div v-auto-animate class="flex gap-10 px-10">
    <button
      class="size-fit self-center rounded-full bg-green-bright px-14 py-14 text-4xl font-bold text-white"
    >
      Да
    </button>
    <div v-if="!isFound" class="pt-7">
      <h2 class="text-center text-3xl text-main-font">
        Похоже, блюда подходящего под ваши фильтры не нашлось... Попробуйте поменять фильтры.
      </h2>
    </div>
    <div v-if="isFound" class="max-w-md basis-9/12 self-center pt-10">
      <img
        class="dish-img h-auto max-w-md rounded-xl object-contain"
        :src="img.content"
        alt="Пюре с котлетой"
      />
    </div>
    <div v-if="isFound" class="flex basis-2/5 flex-col items-center gap-10 font-semibold">
      <h3 class="hyphens-auto break-words break-all text-center text-4xl text-green-darker">
        {{ title }}
      </h3>
      <p class="self-start hyphens-auto break-words break-all text-xl text-main-font">
        {{ description }}
      </p>
    </div>
    <button
      class="size-fit self-center rounded-full bg-another-dish-bg-button px-12 py-14 text-4xl font-bold text-white"
      @click="() => emit('defineRandomDish')"
    >
      Нет
    </button>
  </div>
</template>

<style scoped>
/*
.dish-img{
  width: 300px;
  height: 292px;
} */
</style>
