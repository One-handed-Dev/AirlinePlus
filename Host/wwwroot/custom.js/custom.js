$(document).ready(function(){
    updateCart();
});

const cartCookieName = "cart-items";

function addToCart(id, startDate, endDate, countOfRooms) {
    debugger;
    let products = $.cookie(cartCookieName);

    if (products === undefined)
        products = [];

    else
        products = JSON.parse(products);

    const currentProduct = products.find(x => x.id === id);

    if (currentProduct !== undefined) {
        var newCount = parseInt(currentProduct.count) + parseInt(count);
        var target = products.find(x => x.id === id);
        target.count = newCount;
    }

    else {
        const newProduct = {
            id,
            countOfRooms,
            startDate,
            endDate
        };

        products.push(newProduct);
    }

    $.cookie(cartCookieName, JSON.stringify(products), { expires: 2, path: "/" });
    alert('اتاق موردنظر به سبد خرید شما اضافه شد');
    updateCart();
}

function updateCart() {
    debugger;
    let products = $.cookie(cartCookieName);

    if (products !== undefined) {
        products = JSON.parse(products);

        $("#cart_items_count").text(products.length);
    }
}