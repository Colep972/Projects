const confirmation = document.querySelector("#sent")
const confirmationClose = document.querySelector("#close")
const form = document.querySelector("#contact-form")
let bool = false


if(document.querySelector("#close"))
{
    document.body.style.overflow ="hidden"
}

confirmationClose.addEventListener("click", e =>
{
    confirmation.style.display = "none";
    document.body.style.overflow ="visible"
});


