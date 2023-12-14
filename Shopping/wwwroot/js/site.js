// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let carts = [];
let cartCount = 0;

//Adding eventlistener to listen to the number of clicks for the "Add to Cart" button in the Product Page

var addCarts = document.getElementsByClassName('addToCart');

for (var i = 0; i < addCarts.length; i++) {
    var button = addCarts[i];
    button.addEventListener('click', addToCart);

}

var clicks = document.getElementsByClassName('button');
for (var i = 0; i < clicks.length; i++) {
    var button = clicks[i];
    button.addEventListener('click', click);
}
window.onload = function () {
    
   
    
}


//alert when user click logout
$('#logout').click(function () {
    if (confirm("Are you sure to logout?")) {
        $('#reallogout')[0].click();
    }
});

//The data is package when user click on the addToCart
function addToCart(event) {
    var button = event.target;
    var product = button.parentElement;
    var name = product.getElementsByClassName('product-title')[0].innerText;
    var text = product.getElementsByClassName('product-text')[0].innerText;
    var img = product.getElementsByClassName('product-image')[0].src;
    var price = product.getElementsByClassName('product-price')[0].innerText;
    var productID = product.getElementsByClassName('product-id')[0].value;
  
    createCartItem(name, text, img, price,productID);

}

//When the data is packed, send a request to the server to post the data
function createCartItem(name, text, img, price, productID) {


    let count = document.getElementById("dom");
    cartCount++;
    count.innerHTML = cartCount;



    let xhr = new XMLHttpRequest();
    xhr.open("POST", "/Cart/AddviewCart");
    xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    xhr.onreadystatechange = function () {

    }
    xhr.send("name=" + name + "&text=" + text + "&img=" + img + "&price=" + price + "&productID=" + productID)

}

    function click() {
        var time = event.target.value;
        var productid = event.target.id;
        var total = document.getElementById("row").innerHTML;

        additem(productid, time);
    }

    function additem(productid, time) {
        let xhr = new XMLHttpRequest();
        xhr.open("POST", "/Cart/Gettime");
        xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        xhr.onreadystatechange = function () {
            if (this.readyState == XMLHttpRequest.DONE) {
                if (this.status != 200) {
                    return;
                }
                let data = JSON.parse(this.responseText);

                document.getElementById("row").innerHTML = data.total;
            }


        }
        xhr.send("productid=" + productid + "&time=" + time);
    }


    //Javascript for handling Reviews from Customer
    function handleStarClick(pid, starCount) {

        // Perform AJAX request to send the rating to the server

        $.ajax({
            url: '/browser/addreview',
            type: 'POST',
            data: { productId: pid, starCount: starCount },
            dataType: 'json',
            success: function (response) {
                // Retrieve the updated rating count from the server and add the starcount to it
                $.get('/browser/GetReviewCount', { productId: pid }, function (response) {



                    // Update the rating count with the new average
                    var res = document.getElementById(pid);
                    res.innerHTML = '(You have given ' + starCount + ' stars)' + '<br>' + '(Average: ' + response.averageCount + ' stars ratings)';




                    $('.review-stars i').each(function () {
                        var productid = $(this).data('product-id');
                        var index = $(this).data('index');
                        if (productid == pid && index < response.averageCount) {
                            $(this).addclass('highlight');
                        }
                    });



                });
                //Refresh after 1 second
                setTimeout(function () {
                    location.reload(true);
                }, 1000);
            },
            error: function (error) {
                console.log(error);
            }
        });
    }






