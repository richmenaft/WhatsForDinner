<script setup lang="ts">
import { inject, Ref } from 'vue'
import ListOfDishes from '@/components/List-of-dishes.vue'
import { IDish } from '@/TypeScriptFiles/Interfaces'

interface IProps {
  dishes: Ref<IDish[]>
}

const { dishes } = inject('dishes') as IProps
</script>

<template>
  <section>
    <h3 class="mb-5 text-[32px] text-main-font">Коллекция</h3>

    <ListOfDishes class="mb-10" :dishes="dishes" />
  </section>
</template>

<style scoped></style>
