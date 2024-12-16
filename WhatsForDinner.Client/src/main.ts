import './assets/main.css'

import { createApp } from 'vue'
import { createRouter, createWebHistory, Router } from 'vue-router'
import { autoAnimatePlugin } from '@formkit/auto-animate/vue'

import App from './App.vue'
import Main from '@/pages/Main-page.vue'
import Authentication from '@/pages/Authentication-page.vue'
import Collection from '@/pages/Collection-page.vue'
import History from '@/pages/History-page.vue'

const routes = [
  { path: '/', component: Main },
  { path: '/Authentication', component: Authentication },
  { path: '/Collection', component: Collection },
  { path: '/History', component: History }
]

const router: Router = createRouter({
  history: createWebHistory(),
  routes
})

createApp(App).use(router).use(autoAnimatePlugin).mount('#app')
