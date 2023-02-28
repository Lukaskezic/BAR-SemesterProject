
// displays only selected category
function displayCategory(selectObject) {

    var value = selectObject.value;

    var productList = document.getElementsByClassName("column");

    // Loops through all products
    for (var i = 0; i < productList.length; i++) {

        // If all products should be shown
        if (value === "") 
        {
            productList[i].style.display = "block";
        }
        // If products fits selection
        else if (productList[i].classList.contains(value.split(" "))) 
        {
            
            productList[i].style.display = "block";
        }
        // if product doesn't fits selection
        else
        {
            productList[i].style.display = "none";
        }
    }
}

function checkValue(sender, maxvalue) {
    let inputValue = parseInt(sender.value);
    if (inputValue > maxvalue) {
        sender.value = maxvalue;
    }
    else if (inputValue < 1) {
        sender.value = 1;
    }
}

// Add items to cart
function AddToCart(id, name, ImgURL, price, maxAmount) {

    var productFound = false;
    var AmountToAdd = document.getElementById("amountToAdd " + id).value;
    console.log(AmountToAdd);
    var Products = JSON.parse(localStorage.getItem('Products')) || [];
    //Validate id then add new og add to it,
    Products.forEach(product => {
        if (product["ProductID"] === id) {
            if ((Number(product["Amount"]) + Number(AmountToAdd)) > Number(maxAmount)) {
                alert("You have the maximum number of this product in your cart!")
            }
            else {
                product["Amount"] = Number(product["Amount"]) + Number(AmountToAdd);
            }
            productFound = true;
        }
    });
    if (productFound === false) {
        Products.push({ ProductID: id, Name: name, Amount: AmountToAdd, Url: ImgURL, Price: price });
    }

    // then put it back.
    localStorage.setItem('Products', JSON.stringify(Products));
    
}




