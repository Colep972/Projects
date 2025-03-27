<!-- start Simple Custom CSS and JS -->
<script type="text/javascript">
 

jQuery(document).ready(function( $ ){
    var title = document.querySelector('.awsm-job-form-inner h2');
  	title.textContent = "Postuler à cette offre";
  
  	var fullName = document.querySelector('label[for="awsm-applicant-name"]');
  	fullName.innerHTML = 'Nom et prénom <span class="awsm-job-form-error">*</span>'
  
  	var phone = document.querySelector('label[for="awsm-applicant-phone"]');
  	phone.innerHTML = 'Numéro de téléphone <span class="awsm-job-form-error">*</span>';
  
  	var motivation = document.querySelector('label[for="awsm-cover-letter"]');
  	motivation.innerHTML = 'Lettre de motivation <span class="awsm-job-form-error">*</span>';
  
  	var cv = document.querySelector('label[for="awsm-application-file"]');
  	cv.innerHTML = 'Ajouter un cv <span class="awsm-job-form-error">*</span>';
  
  	var formats = document.querySelector('.awsm-job-form-group small');
  	formats.textContent = "Formats acceptés: .pdf, .doc, .docx";
  
  	var submit = document.querySelector('.awsm-application-submit-btn');
  	submit.value = "Postuler";
  
  	var waitForEl = function(selector, callback) {
      if (jQuery(selector).length) {
        callback();
      } else {
        setTimeout(function() {
          waitForEl(selector, callback);
        }, 100);
      }
    };
  
  	waitForEl('#awsm-applicant-name-error', function() {
      let elt = document.getElementById('awsm-applicant-name-error');
      elt.textContent = "Ce champ est requis.";
    });
  	waitForEl('#awsm-applicant-email-error', function() {
      let elt = document.getElementById('awsm-applicant-email-error');
      elt.textContent = "Ce champ est requis.";
    });
    waitForEl('#awsm-applicant-phone-error', function() {
        let elt = document.getElementById('awsm-applicant-phone-error');
        elt.textContent = "Ce champ est requis.";
    });
  	waitForEl('#awsm-cover-letter-error', function() {
      let elt = document.getElementById('awsm-cover-letter-error');
      elt.textContent = "Ce champ est requis.";
    });
  	waitForEl('#awsm-application-file-error', function() {
      let elt = document.getElementById('awsm-application-file-error');
      elt.textContent = "Ce champ est requis.";
    });
  	waitForEl('#awsm_form_privacy_policy-error', function() {
      let elt = document.getElementById('awsm_form_privacy_policy-error');
      elt.textContent = "Ce champ est requis.";
    });
  
  	waitForEl('.awsm-application-message.awsm-success-message p', function() {
      let elt = document.querySelector('.awsm-application-message.awsm-success-message p');
      elt.textContent = "Votre demande a été soumise.";
    })
});</script>
<!-- end Simple Custom CSS and JS -->
