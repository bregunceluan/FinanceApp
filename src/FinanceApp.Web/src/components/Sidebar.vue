<script setup>
import router from '@/router';

const drawer =ref(true)
const rail =ref(true)


const items = [
{icon:"mdi-view-dashboard",text:"Dashboard",goto:"/dash"},
{icon:"mdi-cash-multiple",text:"Expanses",goto:"/expanses"},
{icon:"mdi-finance",text:"Investiments",goto:"/investments"},
]
</script>

<template>
    <nav>      
        <v-navigation-drawer v-model="drawer" :rail="rail" permanent @click="rail = false">
            <v-list-item prepend-avatar="https://randomuser.me/api/portraits/men/92.jpg">
                <template v-slot:append>
                    <v-btn icon="mdi-chevron-left"
                    variant="text"
                    @click.stop="rail = !rail"
                    ></v-btn>
                </template>
            </v-list-item>
            <v-divider></v-divider>
            
            <v-list>
                <v-list-item v-for="(item,i) in items" :key="i" :value="item" color="primary" @click="() => router.push(item.goto)">
                    <template v-slot:prepend>
                        <v-icon :icon="item.icon"></v-icon>
                    </template>
                    <v-list-item-title v-text="item.text"></v-list-item-title>
                </v-list-item>
            </v-list>
            
        </v-navigation-drawer>
    </nav>
</template>

