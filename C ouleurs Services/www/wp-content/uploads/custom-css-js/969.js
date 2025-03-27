<!-- start Simple Custom CSS and JS -->
<script type="text/javascript">
 

jQuery(document).ready(function( $ ){
    var details = document.querySelectorAll('span.awsm-job-more');
  	details.forEach(detail => detail.textContent = "Plus de détails");
  
  	var search = document.querySelector('input[name="jq"]');
  	search.placeholder = "Rechercher";
  
  	var label = document.querySelector('.label');
  	label.textContent = "Tous les types d'emplois";
  
  	var emplois = document.querySelector('.awsm-selectric-scroll ul li:first-child');
  	emplois.textContent = "Tous les types d'emplois";
});</script>
<!-- end Simple Custom CSS and JS -->
