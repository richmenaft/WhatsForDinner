<script setup lang="ts">
import { ref } from 'vue'

const isAuthenticationProcess = ref<boolean>(true)
const isRegisterProcess = ref<boolean>(false)

function changeProcess() {
  isAuthenticationProcess.value = !isAuthenticationProcess.value
  isRegisterProcess.value = !isRegisterProcess.value
}
</script>

<template>
  <div v-if="isAuthenticationProcess" class="flex justify-center">
    <div class="w-1/2 border-8 border-green-bright px-32 py-10">
      <h2 class="mb-10 text-center text-3xl">Войти в аккаунт</h2>
      <form>
        <label class="mb-4 block">
          <span class="text-gray-700">Адрес электронной почты</span>
          <input
            type="email"
            class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-300 focus:ring focus:ring-green-bright focus:ring-opacity-50"
            placeholder="john@example.com"
          />
        </label>
        <label class="mb-4 block">
          <span class="text-gray-700">Пароль</span>
          <input
            type="password"
            class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-300 focus:ring focus:ring-green-bright focus:ring-opacity-50"
          />
        </label>
        <p class="mb-2">Не получается войти?</p>
        <p class="mb-6">
          Ещё не с нами?
          <span class="cursor-pointer text-green-darker" @click="changeProcess()"
            >Зарегистрироваться</span
          >
        </p>
        <button type="submit" class="w-full rounded-full border border-slate-300 p-3 text-2xl">
          Войти
        </button>
      </form>
    </div>
  </div>
  <div v-if="isRegisterProcess" class="flex justify-center">
    <div class="w-1/2 border-8 border-green-bright px-32 py-10">
      <h2 class="mb-10 text-center text-3xl">Регистрация</h2>
      <form>
        <label class="mb-4 block">
          <span class="text-gray-700">Имя пользователя </span>
          <input
            type="text"
            class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-300 focus:ring focus:ring-green-bright focus:ring-opacity-50"
            placeholder="Крутой_перец"
          />
        </label>
        <label class="mb-4 block">
          <span class="text-gray-700">Адрес электронной почты</span>
          <input
            type="email"
            class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-300 focus:ring focus:ring-green-bright focus:ring-opacity-50"
            placeholder="john@example.com"
          />
        </label>
        <label class="mb-4 block">
          <span class="text-gray-700">Пароль</span>
          <input
            type="password"
            class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-300 focus:ring focus:ring-green-bright focus:ring-opacity-50"
          />
        </label>
        <label class="mb-6 block">
          <span class="text-gray-700">Повторите пароль</span>
          <input
            type="password"
            class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-300 focus:ring focus:ring-green-bright focus:ring-opacity-50"
          />
        </label>
        <p class="mb-6">
          Уже есть аккаунт?
          <span class="cursor-pointer text-green-darker" @click="changeProcess()">Войти</span>
        </p>
        <button type="submit" class="w-full rounded-full border border-slate-300 p-3 text-2xl">
          Зарегистрироваться
        </button>
      </form>
    </div>
  </div>
</template>

<style scoped></style>
