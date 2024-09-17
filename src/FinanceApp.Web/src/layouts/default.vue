<template>
  <v-app>
    <v-main>
      <router-view />
    </v-main>
    <Sidebar v-if="!$route.path.includes('login')"></Sidebar>
  </v-app>
</template>

<script setup>

</script>


