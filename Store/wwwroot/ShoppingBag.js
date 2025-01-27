let cart = JSON.parse(sessionStorage.getItem("cart")) || []
let price = 0
const load = addEventListener("load", async () => {
    drawCart()
}
)


const drawCart = async () => {
    price=0
    for (let i = 0; i < cart.length; i++) {//forEach is nicer
        price+=cart[i].price
        drawOneItem(cart[i])
    }
    totalAmountAndPrice(price)
}
const totalAmountAndPrice = () => {
    document.getElementById("totalAmount").innerHTML = price + 'ש"ח'
    document.getElementById("itemCount").innerHTML = cart.length;

}

const drawOneItem = async (product) => {
    let tmp = document.getElementById("temp-row");
    let cloneProduct = tmp.content.cloneNode(true)
    let url = `./Images/${product.image}`
    cloneProduct.querySelector(".image").style.backgroundImage = `url(${url})`
    cloneProduct.querySelector(".itemName").textContent = product.productName
    cloneProduct.querySelector(".price").innerText = product.price + ' ₪'
    cloneProduct.querySelector(".expandoHeight").addEventListener('click', () => { deleteItem(product) })
    document.querySelector("tbody").appendChild(cloneProduct)

}
const deleteItem = (product) => {
    alert("dddddddddddddddddd")
  
    const index = cart.findIndex(p => p.productName == product.productName)
    if (index > -1) {
        cart.splice(index, 1)
        sessionStorage.setItem("cart", JSON.stringify(cart))
    }
    document.querySelector("tbody").innerHTML = ""
    drawCart()
}

const addOrder = async () => {
    let order = createOrder()
    try {
        const data = await fetch('api/orders', {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(order)
        }
        )
        //const dataOrder = await data.json()
        //console.log("dataOrder======================================================", dataOrder)
       
        sessionStorage.setItem("cart", JSON.stringify([]))
        alert("sucsseful order!!!!!!!!!!!!!")
        window.location.href = "Products.html"
       
    }
    catch (error) {
        alert(error)
    }

}

const placeOrder = async () => {
    if (sessionStorage.getItem("userId")) {
        addOrder()
    }
    else {
        alert("please register")
        window.location.href = "login.html"
    }
}
const createOrder = () => {
    let orderItemsList = cart.map(i => { return { Quantity: 1, ProductId: i.id } })
    let order = {
        "OrderDate": "2025-01-01",
        "OrderSum": price,
        "UserId": JSON.parse(sessionStorage.getItem("userId")) || "",
        "OrderItems": orderItemsList
    }
    return order;
}








