let section2Slideshow = 1;
let section5Slideshow = 1;
const section2Follow = document.querySelectorAll(".fade");
const section2Before = document.querySelector("#section2-before");
const section2After = document.querySelector("#section2-after");
const section5Number = document.querySelectorAll(".section5-right-number");
const section5Title = document.querySelectorAll(".section5-right-title");
const section5Text = document.querySelectorAll(".section5-right-text");
const section5Before = document.querySelector("#section5-before");
const section5After = document.querySelector("#section5-after");
showSection2(section2Slideshow);
showSection5(section5Slideshow);

section2Before.addEventListener("click", () =>
{
    showSection2(section2Slideshow += -1)
})

section2After.addEventListener("click",() =>
{
    showSection2(section2Slideshow += 1)
})

function showSection2(n)
{
    let slideshowImg = document.querySelectorAll(".Actu");
    if(n === 1)
    {
        section2Before.style.display = "none";
    }
    else
    {
        section2Before.style.display = "block";
    }
    if (n === slideshowImg.length)
    {
        section2After.style.display = "none";
    }
    else
    {
        section2After.style.display = "block";
    }
    for (let i = 0; i < slideshowImg.length; ++i)
    {
        slideshowImg[i].style.opacity = "0";
    }
    slideshowImg[section2Slideshow-1].style.opacity = "1";
    for (let i = 0; i < section2Follow.length; ++i)
    {
        section2Follow[i].style.fill = "none";
    }
    section2Follow[section2Slideshow-1].style.fill = "#A83815";
}

section5Before.addEventListener("click", () =>
{
    showSection5(section5Slideshow += -1)
})

section5After.addEventListener("click",() =>
{
    showSection5(section5Slideshow += 1)
})

function showSection5(n)
{
    if(n === 1)
    {
        section5Before.style.display = "none";
    }
    else
    {
        section5Before.style.display = "block";
    }
    if (n === section5Number.length)
    {
        section5After.style.display = "none";
    }
    else
    {
        section5After.style.display = "block";
    }
    if (n > section5Number.length)
    {
        section5Slideshow = 1;
    }
    if (n < 1)
    {
        section5Slideshow = section5Number.length;
    }
    if (n > 1)
    {
        document.querySelector(".section5-right-link").style.display = "none";
    }
    else
    {
        document.querySelector(".section5-right-link").style.display = "block";
    }
    for(let i = 0; i < section5Number.length; ++i)
    {
        section5Number[i].style.opacity = "0";
        section5Title[i].style.opacity = "0";
        section5Text[i].style.opacity = "0";

    }
    section5Number[section5Slideshow-1].style.opacity = "1";
    section5Title[section5Slideshow-1].style.opacity = "1";
    section5Text[section5Slideshow-1].style.opacity = "1";
}