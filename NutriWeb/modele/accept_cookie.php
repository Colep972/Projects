<?php 
	setcookie('accept_cookie',true,time()+12*3600,'/',null,false,true);
	
	if(isset($_SERVER['HTTP_REFERER']) AND !empty($_SERVER['HTTP_REFERER']))
	{
		header('Location:'.$_SERVER['HTTP_REFERER'].'');
		exit();
	}
	else
	{
		header('Location:../index.php');
		exit();
	}
?>
