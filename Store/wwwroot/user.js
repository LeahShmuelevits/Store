sessionStorage.getItem("id")
const getDataFromRegister = () => {
    const username = document.querySelector("#username").value
    const password = document.querySelector("#password2").value
    const firstname = document.querySelector("#firstname").value
    const lastname = document.querySelector("#lastname").value
    return { username, password, firstname, lastname }
}
const getDataFromUpdate = () => {
    const username = document.querySelector("#usernameOnUpdate").value
    const password = document.querySelector("#passwordOnUpdate").value
    const firstname = document.querySelector("#firstnameOnUpdate").value
    const lastname = document.querySelector("#lastnameOnUpdate").value
    const userId = sessionStorage.getItem("userId");
    return { userId, username, password, firstname, lastname }
}
const getDataFromLogin = () => {
    const username = document.querySelector("#nameInput").value
    const password = document.querySelector("#passwordInput").value
    const firstname = "no-name"
    const lastname = "no-name"
    return { username, password, firstname, lastname }
}
const login = async () => {
    const user = getDataFromLogin()
    
    try {
        const data = await fetch(`api/Users/login/?username=${user.username}&password=${user.password}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            query: {
                username: user.username,
                password: user.password
            }
        });
        if (data.status == 400) {
            alert(" cant register with bad password")
        }
        if (data.status == 204) {
           throw new Error("user not found")
        }
        if (data.status == 400) {
            throw new Error("all fields are required")
        }
        console.log(data)
        const dataLogin = await data.json()
        console.log('post data',dataLogin)
        sessionStorage.setItem("userId", dataLogin.userId)
        window.location.href = 'Products.html'
       

    }
    catch (error) {
        console.log(error)
        alert(error) 
    }
    
}
const newUser = () => {
    const container = document.querySelector(".container")
    container.classList.remove("container")
}

const seeTheUpdateUser = () => {
    const container = document.querySelector(".containerOfUpdate")
    container.classList.remove("containerOfUpdate")
}

const register = async () => {
    const user = getDataFromRegister()
    console.log(user.password.length)
    try {   
        const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!emailPattern.test(user.username)) {
            alert("The username must be a valid email address.");
            return;
        }
        if (user.username.length > 50) {
            throw new Error("Username maximum 50")
        }
        if (user.password < 5 || user.password > 20) {
            throw new Error("password must be between 5 to 20")
        }
        if (user.username == null || user.password == null) {
            throw new Error("This fields are requierd")
        }
        const postFromData = await fetch("api/Users", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(user)
        });
        if (postFromData.status == 400) {
            alert("all fields are required")
        }
        if (postFromData.status == 422) {
            alert("cant register with bad password")
        }

        const dataPost = await postFromData.json()
        console.log('post data', dataPost)
        alert(`user num ${dataPost.userId} sucsseful register please login now!!!!!!!!!!!!!`)

       
    }
    catch (error) {
        alert(error)
    }
}
const updateUser = async () => {
    console.log(sessionStorage.getItem("userId"))
    const user = getDataFromUpdate()
    try {
        const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!emailPattern.test(user.username)) {
            alert("The username must be a valid email address.");
            return;
        }
        if (user.username.length > 50) {
            throw new Error("Username maximum 50")
        }
        if (user.password < 5 || user.password > 20) {
            throw new Error("password must be between 5 to 20")
        }
        if (user.username == null || user.password == null) {
            throw new Error("This fields are requierd")
        }
        console.log(sessionStorage.getItem("userId"))
        const updateFromData = await fetch(`api/Users/${sessionStorage.getItem("userId")}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(user)
        });
        if (updateFromData.status == 400) {
            throw new Error("cant update with bad password")
        }
        alert(`user ${sessionStorage.getItem("userId")} update`)
         window.location.href = 'Products.html'
    }
    catch (error) {
        alert(error)
    }
}
const CheckPassword = async () => {
    const progress = document.querySelector("#progress")
    const password2 = document.querySelector("#password2").value
    if (password2.length > 20 || password2.length < 5) {
        alert("the password must be between 5 to 20")
    }
    try {
        
        const postProgress = await fetch("api/Users/CheckPassword", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(password2)
        });
      
        
        if (postProgress.status == 400) {
            alert("dont password!")
        }
        const data = await postProgress.json();
        console.log(data)
        progress.value = data
    } catch (error) {
       
    }
}