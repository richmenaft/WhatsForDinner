<script setup>

</script>

<template>
  <header>
    <div class="bg-green-bright">
      <div class="w-4/5 m-auto flex flex-auto justify-between py-14 mb-6 ">
        <h1 class="font-bold main-header text-main-font">
          <router-link to="/">Что на <span class="text-white">ужин?</span></router-link>
        </h1>
        <div class="flex flex-auto gap-5 justify-end">
          <h1 class="font-bold main-header text-main-font">
            <router-link to="/Collection">Коллекция</router-link>
          </h1>
          <h1 class="font-bold main-header text-main-font">
            <router-link to="/History">История</router-link>
          </h1>
          <h1 class="font-bold main-header text-main-font">
            <router-link to="/Authentication">Войти</router-link>
          </h1>
        </div>
      </div>
    </div>
  </header>
</template>

<style scoped>
.main-header{
  font-size: 40px;
}
</style>