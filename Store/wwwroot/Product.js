
//לשנות לפי הערות של פנסטר גם את הפונציה של השינו וגם להעביר את הבדיקה מהקונרולר לסרביס

const load = addEventListener("load", async () => {
    drawProductList()
    getCategories()
})


//enter to a new function?
let categories = []//let?
let cart = JSON.parse(sessionStorage.getItem("cart"))||[]
sessionStorage.setItem("categories", JSON.stringify(categories))
sessionStorage.setItem("cart", JSON.stringify(cart))
document.querySelector("#ItemsCountText").innerHTML = cart.length

//sessionStorage.setItem("cart", JSON.stringify(cart))

const getData = async () => {



    document.getElementById("PoductList").innerHTML = ''

    const minPrice = document.querySelector("#minPrice").value
    const maxPrice = document.querySelector("#maxPrice").value
    const nameSearch = document.querySelector("#nameSearch").value

    return { nameSearch, minPrice, maxPrice }
}


const filterProducts=() => {
    drawProductList()
}

const drawProductList = async () => {

    let categoryIds1 = JSON.parse(sessionStorage.getItem("categories"))//const?

    let { nameSearch, minPrice, maxPrice } = await getData();
    let url = "api/products"
    if (nameSearch || minPrice || maxPrice || categoryIds1)
        url += '?'
    if (nameSearch != '')
        url += `&desc=${nameSearch}`//מה זה הDESC
    if (minPrice != '')
        url += `&minPrice=${minPrice}`
    if (maxPrice != '')
        url += `&maxPrice=${maxPrice}`
    if (categoryIds1.length != 0) {
        for (let i = 0; i < categoryIds1.length; i++)
            url += `&categoryIds=${categoryIds1[i]}`
    }

    try {
        const allProducts = await fetch(url, {
            method: 'Get',
            headers: {
                'Content-Type': 'application/json'
            },
            query: {
                desc: nameSearch,
                minPrice: minPrice,
                maxPrice: maxPrice,
                categoryIds:categoryIds1
            }
        });
        if (allProducts.ok) {//לעשות עוד סטטוסים
            const dataProducts = await allProducts.json()
            console.log("dataProducts", dataProducts)
            showAllProducts(dataProducts)
        }
    }
    catch (error) {
        alert(error)
    }

}
const showAllProducts = async (products) => {
    for (let i = 0; i < products.length; i++) {
        showOneProduct(products[i])
    }
}
const getCategories = async () => {
    try {
        const allCategories = await fetch("api/Categories", {
            method: 'Get',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        if (allCategories.ok) {//לעשות עוד סטטוסים(לבדוק אם אין קטגוריות שיביא מערך ריק?)
            const dataCategories = await allCategories.json()
            console.log("dataCategories", dataCategories)
            showAllCategories(dataCategories)

        }

    }
    catch(error) {
        alert(error)
    }
}


const showAllCategories = async (categories) => {
    for (let i = 0; i < categories.length; i++) {
        showOneCategory(categories[i])
    }
}

const showOneCategory = async (category) => {
    let tmp = document.getElementById("temp-category");
    let cloneProduct = tmp.content.cloneNode(true)
    cloneProduct.querySelector(".OptionName").textContent = category.categoryName
    cloneProduct.querySelector(".opt").addEventListener('change', () => {chooseCategories(category.id) })//איך לכץתוב את הID
    document.getElementById("categoryList").appendChild(cloneProduct)

}

const chooseCategories = (categoryId) => {
    let categoriesTmp = JSON.parse(sessionStorage.getItem("categories"))
    let index = categoriesTmp.indexOf(categoryId)
    if (index == -1)
        categoriesTmp.push(categoryId)
    else
        categoriesTmp.splice(index, 1)
    sessionStorage.setItem("categories", JSON.stringify(categoriesTmp))
    drawProductList()
    }



const showOneProduct = async (product) => {
    let tmp = document.getElementById("temp-card");
    let cloneProduct = tmp.content.cloneNode(true)
    cloneProduct.querySelector("img").src = "./Images/" + product.image
    cloneProduct.querySelector("h1").textContent = product.productName
    cloneProduct.querySelector(".price").innerText = product.price
    cloneProduct.querySelector(".description").innerText = product.description
    cloneProduct.querySelector("button").addEventListener('click', () => { addToCart(product) })
    document.getElementById("PoductList").appendChild(cloneProduct)
}


const addToCart = (product) => {
    alert("product add to your cart")
    let cartTemp = JSON.parse(sessionStorage.getItem("cart"))
    cartTemp.push(product)
    sessionStorage.setItem("cart", JSON.stringify(cartTemp))
    document.querySelector("#ItemsCountText").innerHTML=cartTemp.length
}
const toUpdate = () => {
    if (!JSON.parse(sessionStorage.getItem("userId"))) {
        alert("you have not logged in yet");
        window.location.href = "login.html";
    }
    else {
        window.location.href = "userDetails.html";
    }
}
