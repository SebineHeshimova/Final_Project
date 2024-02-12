let addToBasket = document.querySelectorAll(".add-to-basket");
addToBasket.forEach(btn => btn.addEventListener("click", function (e) {
    let url = btn.getAttribute("href");

    e.preventDefault();

    fetch(url)
        .then(response => {
            if (response.status == 200) {
                alert("added to basket")
            }
            else {
                alert("Error")
            }
        })
}))