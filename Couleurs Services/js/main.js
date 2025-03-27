width = document.body.clientWidth
const toTheTop = document.querySelector("#top")
const home = document.querySelector("#header-home")
const test = document.querySelector("#test")
const selectHeader = document.querySelector("#header-prestations")
const selectFooter = document.querySelector("#footer-prestations")
const overlay = document.querySelector(".overlay")
const nav = document.querySelector("#nav-container")
const hamburger = document.querySelector("#hamburger")
const logo = document.querySelector("#logo")
const above = document.querySelector("#above-header")
const divMail = document.querySelector("#nav-items-mail-img")
const divMobile = document.querySelector("#nav-items-mobile-img")
const divFooterMobile = document.querySelector("#social-links")
const footerButton = document.querySelector("#navigation-link")
const footerDivNavigation = document.querySelector("#navigation-container")
const contact = document.querySelector("#contact")
let click = 0
let cmptHeader = 0
let cmptFooter = 0
const mailIcon = document.createElement("img")
const mobileIcon = document.createElement("img")
const youtubeIcon = document.createElement("img")
const linkedinIcon = document.createElement("img")
const footerLogo = document.createElement("img")
const mailLink = document.createElement("a")
const mobileLink = document.createElement("a")
const youtubeLink = document.createElement("a")
const linkedinLink = document.createElement("a")
const newFooterButton = document.createElement("a")
const footerLogoContainer = document.createElement("div")
const footer = document.querySelector("footer")
const body = document.querySelector("body")



mailIcon.src = "../data/icon_mail.svg"
mobileIcon.src = "../data/icon_phone.svg"
youtubeIcon.src = "../data/yt.svg"
linkedinIcon.src = "../data/Linkedin.svg"
mailLink.href = "mailto:info@couleurs-services.com"
mailLink.innerHTML = "info@couleurs-services.com"
mobileLink.href = "tel:0982515974";
mobileLink.innerHTML = "0982515974";
youtubeLink.href= "https://www.youtube.com/@Couleurs-Services"
youtubeLink.append(youtubeIcon);
linkedinLink.href = "https://www.linkedin.com/company/couleursservices/"
linkedinLink.append(linkedinIcon);
mailLink.style.color="#E7663D"
mailLink.style.fontSize = "20px";
mailLink.style.textDecoration="none"
mobileLink.style.color="#E7663D"
mobileLink.style.textDecoration="none"
mobileLink.style.fontSize = "20px";
contact.style.fontSize = "20px";
newFooterButton.href = ""
newFooterButton.innerHTML = "Prendre rendez-vous"
footerLogoContainer.classList.add("vanish")


window.addEventListener("resize", e =>
{
	width = document.body.clientWidth
	responsiv()
});

responsiv()

toTheTop.addEventListener("click", e =>
{
	window.scrollTo({top: 0, behavior: 'smooth'});
});

selectHeader.addEventListener("click", e =>
{
	cmptHeader++
	if (cmptHeader %2 === 0 && selectHeader.selectedIndex != 1 && selectHeader.selectedIndex != 2 && selectHeader.selectedIndex != 3)
	{
		window.location.href = selectHeader.options[selectHeader.selectedIndex].value
	}
	else
	{
		console.log("clicked")
	}
})

selectHeader.addEventListener("change", e =>
{
	window.location.href = selectHeader.options[selectHeader.selectedIndex].value
});


selectFooter.addEventListener("click", e =>
{
	cmptFooter++
	if (cmptFooter %2 === 0 && selectFooter.selectedIndex != 1 && selectFooter.selectedIndex != 2 && selectFooter.selectedIndex != 3)
	{
		window.location.href = selectHeader.options[selectHeader.selectedIndex].value
	}
	else
	{
		console.log("clicked")
	}
})

selectFooter.addEventListener("change", e =>
{
    window.location.href = selectFooter.options[selectFooter.selectedIndex].value
});

hamburger.addEventListener("click",clicked);

function responsiv()
{
	if (width >= 335 && width < 1279)
	{
		test.prepend(home)
		divMail.append(mailIcon)
		divMail.append(mailLink)
		divMobile.append(mobileIcon)
		divMobile.append(mobileLink)
		newFooterButton.classList.add("navigation-link")
		//footerDivNavigation.append(newFooterButton)
		divFooterMobile.classList.add("footer-social-list");
		divFooterMobile.append(youtubeLink)
		divFooterMobile.append(linkedinLink)
		//above.style.backgroundColor = "#ffffff"
		divMail.style.display = "flex"
		divMobile.style.display = "flex"
	}
	else
	{
		document.querySelector("#social-links").style.display = "none";
		divMobile.style.display = "none"
		divMail.style.display = "none"
		logo.style.padding = "20px 50px 20px 100px"
		//above.style.backgroundColor = "#E7663D"
		nav.style.gap = "10px"
		document.querySelector("#clone").style.display ="none"
		if (test.children.length < 6)
		{
			document.querySelector("#fifth").style.display = "flex"
		}
		if(test.children.length > 5)
		{
			test.removeChild(home)
			nav.prepend(home)
		}
	}

	if(width >= 1023 && width < 1279)
	{
		footerLogo.src = "../data/logo_footer.svg"
		footerLogoContainer.append(footerLogo)
		footer.prepend(footerLogoContainer)
	}

	if(width < 1023 || width >= 1279)
	{
		footerLogoContainer.style.display = "none"
	}
	else
	{
		footerLogoContainer.style.display = "block"
	}
}

function clicked()
{
	if (width >= 335 && width < 1279)
	{
		click++;
		if (click%2 === 0)
		{
			overlay.style.width = "0";
			hamburger.style.backgroundImage = "url('../data/hamburger_open.svg')"
			document.body.style.overflow ="visible"
			nav.style.flexDirection = "row"
			toTheTop.style.display = "block"
		}
		else
		{
			hamburger.style.backgroundImage = "url('../data/hamburger_close.svg')"
			overlay.style.fontSize = "16px"
			overlay.style.width = "100%"
			nav.style.padding = "20px"
			nav.style.flexDirection = "column"
			nav.style.display = "flex"
			nav.style.alignItems = "flex-start"
			nav.style.justifyContent = "center"
			nav.style.width = "100vw"
			document.body.style.overflow ="hidden"
			document.body.style.height = "100%"
			toTheTop.style.display = "none"
		}
	}
	else
	{
		console.log(width)
	}
}
