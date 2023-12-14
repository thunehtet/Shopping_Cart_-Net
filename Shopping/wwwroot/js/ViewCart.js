window.onload = function () {
    let elem = document.getElementById("checkoutBtn");

    //Alert user if they are ready to check out their cart
    elem.addEventListener("click", function () {
        alert("ready to check out?")

        //send the order cart to server
        sendCartOrder(data);
    })
}