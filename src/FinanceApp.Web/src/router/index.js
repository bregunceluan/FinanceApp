
// Composables
import { createRouter, createWebHistory } from 'vue-router/auto'
import { setupLayouts } from 'virtual:generated-layouts'
import { routes } from 'vue-router/auto-routes'
import cookies from 'vue-cookies'



const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: setupLayouts(routes),
})

// Workaround for https://github.com/vitejs/vite/issues/11804
router.onError((err, to) => {
  if (err?.message?.includes?.('Failed to fetch dynamically imported module')) {
    if (!localStorage.getItem('vuetify:dynamic-reload')) {
      console.log('Reloading page to fix dynamic import error')
      localStorage.setItem('vuetify:dynamic-reload', 'true')
      location.assign(to.fullPath)
    } else {
      console.error('Dynamic import error, reloading page did not fix it', err)
    }
  } else {
    console.error(err)
  }
})

router.isReady().then(() => {
  localStorage.removeItem('vuetify:dynamic-reload')
})

router.beforeEach(async (to,from) =>{
  debugger
  let val = cookies.get('finance')
  if(val ==  null){
    await login()
    await infoUser()
  }
  console.log(from)
})

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
      debugger
      cookies.set("finance",{
          email:JSON.parse(data).email,
          isEmailConfirmed: JSON.parse(data).isEmailConfirmed
      }),
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
              email: "luanbregunce@gmail.com",
              password: "159753Lu@n"
          })
      });
      
      if (!response.ok) {
          console.log(await response.text())
          throw new Error(`HTTP error! status: ${response.status}`);
      }
      const data = await response.text();

      
  } catch (error) {
      console.error('Error fetching data:', error);
  }
}

export default router
