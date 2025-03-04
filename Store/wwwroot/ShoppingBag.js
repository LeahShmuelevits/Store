//let cart = JSON.parse(sessionStorage.getItem("cart")) || []
//let price = 0
//const load = addEventListener("load", async () => {
//    drawCart()
//}
//)


//const drawCart = async () => {
//    price=0
//    cart.forEach(item => {
//        price += item.price;
//        drawOneItem(item);
//    });
//    totalAmountAndPrice(price)
//}
//const totalAmountAndPrice = () => {
//    document.getElementById("totalAmount").innerHTML = price + 'ש"ח'
//    document.getElementById("itemCount").innerHTML = cart.length;

//}

//const drawOneItem = async (product) => {
//    let tmp = document.getElementById("temp-row");
//    let cloneProduct = tmp.content.cloneNode(true)
//    let url = `./Images/${product.image}`
//    cloneProduct.querySelector(".image").style.backgroundImage = `url(${url})`
//    cloneProduct.querySelector(".itemName").textContent = product.productName
//    cloneProduct.querySelector(".price").innerText = product.price + ' ₪'
//    cloneProduct.querySelector(".expandoHeight").addEventListener('click', () => { deleteItem(product) })
//    document.querySelector("tbody").appendChild(cloneProduct)

//}
//const deleteItem = (product) => {
//    alert("dddddddddddddddddd")

//    const index = cart.findIndex(p => p.productName == product.productName)
//    if (index > -1) {
//        cart.splice(index, 1)
//        sessionStorage.setItem("cart", JSON.stringify(cart))
//    }
//    document.querySelector("tbody").innerHTML = ""
//    drawCart()
//}

//const addOrder = async () => {
//    let order = createOrder()
//    try {
//        const data = await fetch('api/orders', {
//            method: "POST",
//            headers: {
//                'Content-Type': 'application/json'
//            },
//            body: JSON.stringify(order)
//        }
//        );
//        let orderData = await data.json();
//        if (data.status == 401) {
//            alert("you cant acomplish the order!!!!!!!")
//        }

//        else {
//          sessionStorage.setItem("cart", JSON.stringify([]))
//          alert(`order num ${orderData.id} sucsseful order!!!!!!!!!!!!!`)
//          window.location.href = "Products.html"
//        }


//    }
//    catch (error) {
//        alert(error)
//    }

//}

//const placeOrder = async () => {
//    if (sessionStorage.getItem("userId")) {
//        addOrder()
//    }
//    else {
//        alert("please register")
//        window.location.href = "login.html"
//    }
//}
//const createOrder = () => {

//    let orderItemsList = cart.map(i => { return { Quantity: 1, ProductId: i.id } })
//    let order = {
//        "OrderDate": "2025-01-01",
//        "OrderSum": price,
//        "UserId": JSON.parse(sessionStorage.getItem("userId")) || "",
//        "OrderItems": orderItemsList
//    }
//    return order;
//}

////let cart = JSON.parse(sessionStorage.getItem("cart")) || [];
////let price = 0;

////// מאזין לאירוע טעינת העמוד
////addEventListener("load", async () => {
////    await drawCart();
////});

////// ציור עגלת הקניות
////const drawCart = async () => {
////    price = 0;
////    document.querySelector("tbody").innerHTML = '';

////    cart.forEach(item => {
////        price += item.price;
////        drawOneItem(item);
////    });

////    updateTotalAmountAndPrice();
////};

////// עדכון הסכום הכולל ומספר הפריטים
////const updateTotalAmountAndPrice = () => {
////    document.getElementById("totalAmount").innerHTML = `${price} ש"ח`;
////    document.getElementById("itemCount").innerHTML = cart.length;
////};

////// ציור פריט אחד בעגלה
////const drawOneItem = (product) => {
////    const template = document.getElementById("temp-row");
////    const clone = template.content.cloneNode(true);
////    const url = `./Images/${product.image}`;

////    clone.querySelector(".image").style.backgroundImage = `url(${url})`;
////    clone.querySelector(".itemName").textContent = product.productName;
////    clone.querySelector(".price").innerText = `${product.price} ₪`;
////    clone.querySelector(".expandoHeight").addEventListener('click', () => { deleteItem(product); });

////    document.querySelector("tbody").appendChild(clone);
////};

////// מחיקת פריט מהעגלה
////const deleteItem = (product) => {
////    const index = cart.findIndex(p => p.productName === product.productName);
////    if (index > -1) {
////        cart.splice(index, 1);
////        sessionStorage.setItem("cart", JSON.stringify(cart));
////    }
////    drawCart();
////};

////// הוספת הזמנה
////const addOrder = async () => {
////    const order = createOrder();
////    try {
////        const response = await fetch('api/orders', {
////            method: "POST",
////            headers: {
////                'Content-Type': 'application/json'
////            },
////            body: JSON.stringify(order)
////        });

////        const orderData = await response.json();

////        if (response.status === 401) {
////            alert("You can't complete the order!");
////        } else {
////            sessionStorage.setItem("cart", JSON.stringify([]));
////            alert(`Order number ${orderData.id} was successful!`);
////            window.location.href = "Products.html";
////        }
////    } catch (error) {
////        alert(error);
////    }
////};

////// ביצוע הזמנה
////const placeOrder = async () => {
////    if (sessionStorage.getItem("userId")) {
////        await addOrder();
////    } else {
////        alert("Please register");
////        window.location.href = "login.html";
////    }
////};

////// יצירת אובייקט הזמנה
////const createOrder = () => {
////    const orderItemsList = cart.map(item => ({ Quantity: 1, ProductId: item.id }));
////    const order = {
////        "OrderDate": new Date().toISOString().split('T')[0], // תאריך נוכחי
////        "OrderSum": price,
////        "UserId": JSON.parse(sessionStorage.getItem("userId")) || "",
////        "OrderItems": orderItemsList
////    };
////    return order;
////};

let cart = JSON.parse(sessionStorage.getItem("cart")) || []
let price = 0
const load = addEventListener("load", async () => {
    drawCart()
}
)


const drawCart = async () => {
    price = 0
    //for (let i = 0; i < cart.length; i++) {
    //    price+=cart[i].price
    //    drawOneItem(cart[i])
    //}

    cart.forEach(item => {
        price += item.price;
        drawOneItem(item);
    });
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
        );
        let orderData = await data.json();
        if (data.status == 401) {
            alert("you cant acomplish the order!!!!!!!")
        }
        //const dataOrder = await data.json()
        //console.log("dataOrder======================================================", dataOrder)
        else {
            sessionStorage.setItem("cart", JSON.stringify([]))
            alert(`order num ${orderData.id} sucsseful order!!!!!!!!!!!!!`)
            window.location.href = "Products.html"
        }


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
