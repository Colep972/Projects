widthqsn = document.body.clientWidth
let slideshow = 1;
let slideshowvideo = 1
let slideshowSecond = 1
const before = document.querySelector("#before");
const after = document.querySelector("#after");
const before1 = document.querySelector("#before1");
const after1 = document.querySelector("#after1");
const before2 = document.querySelector("#before2");
const after2 = document.querySelector("#after2");
const timeline = document.querySelector("#section4-img")

window.addEventListener("resize", e =>
{
    widthqsn = document.body.clientWidth
    responsive()
});
responsive()


function responsive()
{
    if (widthqsn >= 335 && widthqsn < 1023)
    {
        showFirstResponsiv(slideshowvideo)
        timeline.src="../data/timeline.svg"
    }
    else if (widthqsn >= 1023 && widthqsn <= 1279)
    {
        showSecondResponsiv(slideshowSecond)
        timeline.src="../data/Frame582.svg"
    }
    else
    {
        show(slideshow)
        timeline.src="../data/Frame582.svg"
    }
}

before.addEventListener("click", () =>
{
    show(slideshow += -1)
})

after.addEventListener("click",() =>
{
    show(slideshow += 1)
})

before1.addEventListener("click", () =>
{
    showFirstResponsiv(slideshowvideo += -1)
})

after1.addEventListener("click",() =>
{
    showFirstResponsiv(slideshowvideo += 1)
})

before2.addEventListener("click", () =>
{
    showSecondResponsiv(slideshowSecond += -1)
})

after2.addEventListener("click",() =>
{
    showSecondResponsiv(slideshowSecond += 1)
})



function show(n)
{
    let slideshowImg1 = document.querySelectorAll(".section10-img-1");
    let slideshowImg2 = document.querySelectorAll(".section10-img-2");
    let slideshowImg3 = document.querySelectorAll(".section10-img-3");
    if(n === 1)
    {
        before.style.display = "none";
        after.style.display = "block";
    }
    else
    {
        after.style.display = "none";
        before.style.display = "block";
    }
    for (let i = 0; i < slideshowImg2.length; ++i)
    {
        slideshowImg1[i].style.opacity = "0";
        slideshowImg2[i].style.opacity = "0";
        slideshowImg3[i].style.opacity = "0";
    }
    slideshowImg1[1].style.opacity = "0";
    slideshowImg1[slideshow-1].style.opacity = "1";
    slideshowImg2[slideshow-1].style.opacity = "1";
    slideshowImg3[slideshow-1].style.opacity = "1";
}

function showFirstResponsiv(n)
{
    let slideshowImg = document.querySelectorAll(".carrousel-img")
    if(n === 1)
    {
        before1.style.display = "none";
    }
    else
    {
        before1.style.display = "block";
    }
    if (n === slideshowImg.length)
    {
        after1.style.display = "none";
    }
    else
    {
        after1.style.display = "block";
    }
    for (let i = 0; i < slideshowImg.length; ++i)
    {
        slideshowImg[i].style.opacity = "0";
    }
    slideshowImg[slideshowvideo-1].style.opacity = "1";
}

function showSecondResponsiv(n)
{
    let even = document.querySelectorAll(".even")
    let odd = document.querySelectorAll(".odd");
    if(n === 1)
    {
        before2.style.display = "none"
    }
    else
    {
        before2.style.display = "block"
    }
    if (n === even.length)
    {
        after2.style.display = "none"
    }
    else
    {
        after2.style.display = "block"
    }

    for (let i = 0; i < even.length; ++i)
    {
        even[i].style.opacity = "0"
    }
    for (let i = 0; i < odd.length; ++i)
    {
        odd[i].style.opacity = "0"
    }
    even[slideshowSecond-1].style.opacity = "1";
    odd[slideshowSecond-1].style.opacity = "1";
}