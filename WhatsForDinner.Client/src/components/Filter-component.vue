<script setup lang="ts">
import { inject, Ref, ref } from 'vue'
import { ITag } from '@/TypeScriptFiles/Interfaces'

let isFilterHintActivated = ref<boolean>(false)

function ShowOrHideFilterHint(): void {
  isFilterHintActivated.value = !isFilterHintActivated.value
}

interface IInjectedTags {
  ingredientTags: Ref<ITag[]>
  typeTags: Ref<ITag[]>
  reasonTags: Ref<ITag[]>
}

const { ingredientTags, typeTags, reasonTags } = inject('dishes') as IInjectedTags

function uncheckAllTags(): void {
  ingredientTags.value.forEach((tag) => {
    tag.state = 0
  })
  typeTags.value.forEach((tag) => {
    tag.state = 0
  })
  reasonTags.value.forEach((tag) => {
    tag.state = 0
  })
}

function changeTagState(tag: ITag): void {
  switch (tag.state) {
    case 0:
      tag.state = 1
      break
    case 1:
      tag.state = -1
      break
    case -1:
      tag.state = 0
      break
  }
}
</script>

<template>
  <div class="flex basis-1/6 flex-col gap-4">
    <div class="flex justify-between pb-6">
      <div v-auto-animate class="flex justify-center gap-6">
        <h3 class="self-center text-[32px] text-main-font">Фильтр</h3>
        <p
          class="cursor-pointer self-center rounded-full bg-filter-question-bg px-4 py-2 text-xl font-semibold text-main-font"
          @click="ShowOrHideFilterHint"
        >
          ?
        </p>
        <div
          v-if="isFilterHintActivated"
          class="size-fit -space-y-1 rounded-xl border border-black bg-white p-3"
        >
          <p class="text-main-font">
            <span class="font-bold">1</span> клик -
            <span class="text-green-darker">выбор</span> параметра
          </p>
          <p class="text-main-font">
            <span class="font-bold">2</span> клика -
            <span class="text-red-700">исключить</span> параметр
          </p>
          <p class="text-main-font"><span class="font-bold">3</span> клика - очистить чекбокс</p>
        </div>
      </div>
      <button
        class="size-fit self-center rounded-full border border-black bg-filter-clear px-10 py-4 text-2xl text-main-font"
        @click="uncheckAllTags()"
      >
        Очистить
      </button>
    </div>
    <div
      v-auto-animate
      class="mb-10 flex w-full rounded-xl border border-filter-border bg-green-bright bg-opacity-20 pb-4 pl-24 pr-4 pt-9 text-2xl text-main-font"
    >
      <div class="flex w-full overflow-y-auto">
        <div class="flex h-96 basis-2/4 flex-col gap-6 filter">
          <h2 class="mb-2 text-2xl text-main-font">Ингредиенты</h2>
          <div class="flex flex-row flex-wrap gap-x-24 gap-y-6 pr-8">
            <div v-for="(tag, index) in ingredientTags" :key="index">
              <div class="filter-element flex items-center gap-2">
                <p
                  v-if="tag.state === -1"
                  class="size-fit h-[40px] w-[40px] cursor-pointer rounded-lg border border-black bg-filter-X pt-1 text-center"
                  @click="changeTagState(tag)"
                >
                  X
                </p>
                <p
                  v-if="tag.state === 1"
                  class="size-fit h-[40px] w-[40px] cursor-pointer rounded-lg border border-black bg-filter-V-bg pt-1 text-center text-filter-V-text"
                  @click="changeTagState(tag)"
                >
                  V
                </p>
                <p
                  v-if="tag.state === 0"
                  class="size-fit h-[40px] w-[40px] cursor-pointer rounded-lg border border-black bg-white pt-1 text-center"
                  @click="changeTagState(tag)"
                ></p>
                <p class="w-16 self-center">{{ tag.title }}</p>
              </div>
            </div>
          </div>
        </div>
        <div class="flex h-96 w-full basis-1/4 flex-col gap-6 filter">
          <h2 class="mb-2 text-2xl text-main-font">Тип</h2>
          <div class="flex flex-col gap-y-6 pr-8">
            <div v-for="(tag, index) in typeTags" :key="index">
              <div class="filter-element flex items-center gap-2">
                <p
                  v-if="tag.state === -1"
                  class="size-fit h-[40px] w-[40px] cursor-pointer rounded-lg border border-black bg-filter-X pt-1 text-center"
                  @click="changeTagState(tag)"
                >
                  X
                </p>
                <p
                  v-if="tag.state === 1"
                  class="size-fit h-[40px] w-[40px] cursor-pointer rounded-lg border border-black bg-filter-V-bg pt-1 text-center text-filter-V-text"
                  @click="changeTagState(tag)"
                >
                  V
                </p>
                <p
                  v-if="tag.state === 0"
                  class="size-fit h-[40px] w-[40px] cursor-pointer rounded-lg border border-black bg-white pt-1 text-center"
                  @click="changeTagState(tag)"
                ></p>
                <p class="self-center">{{ tag.title }}</p>
              </div>
            </div>
          </div>
        </div>
        <div class="flex h-96 w-full basis-1/4 flex-col gap-6 filter">
          <h2 class="mb-2 text-2xl text-main-font">Повод</h2>
          <div class="flex flex-col gap-y-6 pr-8">
            <div v-for="(tag, index) in reasonTags" :key="index">
              <div class="filter-element flex items-center gap-2">
                <p
                  v-if="tag.state === -1"
                  class="size-fit h-[40px] w-[40px] cursor-pointer rounded-lg border border-black bg-filter-X pt-1 text-center"
                  @click="changeTagState(tag)"
                >
                  X
                </p>
                <p
                  v-if="tag.state === 1"
                  class="size-fit h-[40px] w-[40px] cursor-pointer rounded-lg border border-black bg-filter-V-bg pt-1 text-center text-filter-V-text"
                  @click="changeTagState(tag)"
                >
                  V
                </p>
                <p
                  v-if="tag.state === 0"
                  class="size-fit h-[40px] w-[40px] cursor-pointer rounded-lg border border-black bg-white pt-1 text-center"
                  @click="changeTagState(tag)"
                ></p>
                <p class="self-center">{{ tag.title }}</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
::-webkit-scrollbar-track {
  margin-top: 5px;
}

::-webkit-scrollbar-track {
  border-radius: 10px;
}

.filter-element {
  font-size: 20px;
}

.filter li {
  margin-bottom: 15px;
}
</style>
