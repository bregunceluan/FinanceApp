<template>
    <div class="login-container">
        <h1>Login</h1>
        <form @submit.prevent="handleLogin">
            <input v-model="username" type="text" placeholder="Username" required />
            <input v-model="password" type="password" placeholder="Password" required />
            <button type="submit">Login</button>
        </form>
    </div>
</template>

<script setup>

import { useAuth } from '@/stores/auth';
import { onMounted } from 'vue';
import { useRouter } from 'vue-router';

const rout = useRouter();

const auth = useAuth();

async function infoUser() {
    try {
        const response = await fetch('http://localhost:5252/v1/identity/manage/info', {
            method: 'GET',
            credentials:'include'
        });
        
        if (!response.ok) {
            console.log(await response.text())
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        const data = await response.text();
        console.log(data);
    } catch (error) {
        console.error('Error fetching data:', error);
    }
}

async function health() {
    try {
        const response = await fetch('http://localhost:5252/health', {
            method: 'GET',
            credentials:'include'
        });
        
        if (!response.ok) {
            console.log(await response.text())
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        const data = await response.text();
        console.log(data);
    } catch (error) {
        console.error('Error fetching data:', error);
    }
}

async function login() {
    try {
        const response = await fetch('http://localhost:5252/v1/identity/login?useCookies=true', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body:JSON.stringify({
                email: "",
                password: ""
            })
        });
        
        if (!response.ok) {
            console.log(await response.text())
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        const data = await response.text();
        console.log(data);
    } catch (error) {
        console.error('Error fetching data:', error);
    }
}


onMounted(async (a) =>{    
    await health()
    
})



</script>

<style scoped>

.login-container {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    height: 100vh;
    background-color: transparent;
    font-family: Arial, sans-serif;
    padding: 0 20px;
}

h1 {
    margin-bottom: 20px;
    color: #333;
}

form {
    display: flex;
    flex-direction: column;
    width: 100%;
    max-width: 300px;
}

input {
    padding: 10px;
    margin-bottom: 10px;
    border: 1px solid #ccc;
    border-radius: 4px;
}

button {
    padding: 10px;
    background-color: #007bff;
    color: white;
    border: none;
    border-radius: 4px;
    cursor: pointer;
}

button:hover {
    background-color: #0056b3;
}

@media (max-width: 480px) {
    h1 {
        font-size: 1.5em;
    }
    
    input, button {
        font-size: 1em;
    }
}
</style>
