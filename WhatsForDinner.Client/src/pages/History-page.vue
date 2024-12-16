<script setup lang="ts">
import { onMounted, ref } from 'vue'
import axios from 'axios'

const hello = ref<string>('Привет')

onMounted(async () => {
  axios
    .get('http://localhost:5262/api/fruit/getfruit')
    .then((response) => (hello.value = response.data))
    .then((response) => {
      console.log(response)
    })
    .catch((error) => {
      console.error(error)
    })
})
</script>

<template>
  <p>{{ hello }}</p>
</template>

<style scoped></style>
