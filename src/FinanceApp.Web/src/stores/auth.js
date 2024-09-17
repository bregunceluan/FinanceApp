import { defineStore } from 'pinia'
import cookies from 'vue-cookies'


export const useAuth = defineStore('auth', {
    state:() =>{
        isAuthenticated: false
    }
})

