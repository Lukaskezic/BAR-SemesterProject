
function generateKurv() {

    var totalPrice = 0;

    var productDiv = document.getElementById("result");
    var products = JSON.parse(localStorage.getItem("Products"));
    if (products.length === 0) {
        document.getElementById("cartWrapper").hidden = true;
        document.getElementById("cartEmpty").hidden = false;
    }
    else {
        document.getElementById("cartWrapper").hidden = false;
        document.getElementById("cartEmpty").hidden = true;
    }

    products.forEach(product => {
        // Creates Surrounding div
        var tr = document.createElement("TR");
        tr.id = "tr" + product["ProductID"];

        // Div image
        var imgDiv = document.createElement("td");
        imgDiv.id = "imgDiv" + product["ProductID"];
        imgDiv.style.height = "18vw";
        //imgDiv.style.maxHeight = "5vh";
        imgDiv.style.width = "18vw";
        tr.appendChild(imgDiv);

        // Creates image
        var img = document.createElement("IMG");
        var imgURL = document.createAttribute("src");
        imgURL.value = product["Url"];
        img.style.display = "block";
        img.setAttributeNode(imgURL);
        imgDiv.appendChild(img);

        // Name of product
        var productname = document.createElement("TD");
        productname.innerText = product["Name"];
        tr.appendChild(productname);

        // Display amount of product
        var amount = document.createElement("td");
        amount.id = "Amount" + product["ProductID"];
        amount.innerText = product["Amount"];
        tr.appendChild(amount);

        // Display price of product in total
        var price = document.createElement("td");
        price.id = "Price" + product["ProductID"];
        price.innerText = (product["Price"] * product["Amount"]);
        tr.appendChild(price);

        totalPrice += (product["Price"] * product["Amount"]);
     

        // Creates Button to remove item
        var td = document.createElement("TD");
        var rmvBtn = document.createElement("BUTTON");
        rmvBtn.innerText = "Remove product";
        var func = document.createAttribute("onclick");
        func.value = "removeItem(" + product["ProductID"] + ")";
        rmvBtn.setAttributeNode(func);
        td.appendChild(rmvBtn)
        tr.appendChild(td);


        //console.log(tr);

        // Appends surrounding div to div of all products
        productDiv.appendChild(tr);
    });

    //Total price in p tag
    document.getElementById("totalPrice").innerText = totalPrice;
}


function removeItem(id) {
    var products = JSON.parse(localStorage.getItem("Products"));

    products.forEach(product => {
        if (product["ProductID"] == id) {
            if (product["Amount"] > 1) {
                product["Amount"] = Number(product["Amount"]) - 1;
                document.getElementById("Amount" + id).innerText = product["Amount"];
                document.getElementById("Price" + product["ProductID"]).innerText =(product["Price"] * product["Amount"]);
            }
            else {
                document.getElementById("tr" + product["ProductID"]).remove();
                var index = products.indexOf(product);
                if (index > -1) {
                    products.splice(index, 1);
                }
            }

            document.getElementById("totalPrice").innerText -= product["Price"];

        }
    });
    if (products.length === 0) {
        document.getElementById("cartWrapper").hidden = true;
        document.getElementById("cartEmpty").hidden = false;
    }
    else {
        document.getElementById("cartWrapper").hidden = false;
        document.getElementById("cartEmpty").hidden = true;
    }
    localStorage.setItem("Products", JSON.stringify(products));
}


generateKurv();
