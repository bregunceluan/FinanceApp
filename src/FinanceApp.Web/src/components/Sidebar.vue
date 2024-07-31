<script setup>
const drawer =ref(true)
const rail =ref(true)


const items = [
{icon:"mdi-view-dashboard",text:"Dashboard"},
{icon:"mdi-cash-multiple",text:"Expanses"},
{icon:"mdi-finance",text:"Investiments"},
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
                <v-list-item v-for="(item,i) in items" :key="i" :value="item" color="primary">
                    <template v-slot:prepend>
                        <v-icon :icon="item.icon"></v-icon>
                    </template>
                    <v-list-item-title v-text="item.text"></v-list-item-title>
                </v-list-item>
            </v-list>
            
        </v-navigation-drawer>
    </nav>
</template>

