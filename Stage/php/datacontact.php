<?php
function putData($link,$name,$tel,$email,$date)
{
    if($link)
    {
        $req = "INSERT INTO contact (id,nom,tel,mail,date) VALUES (NULL,'$name','$tel','$email','$date')";
        executeUpdate($link,$req);
    }
}
