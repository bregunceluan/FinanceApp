import { info } from 'sass';
import cookies from 'vue-cookies'

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
        cookies.VueCookies.set("finance",{
            email:data.email,
            isEmailConfirmed: data.isEmailConfirmed
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
        
        debugger
        
        
        if (!response.ok) {
            console.log(await response.text())
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        const data = await response.text();
        
        
    } catch (error) {
        console.error('Error fetching data:', error);
    }
}

export default login