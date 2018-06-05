# Fortimporter
<img src="https://portforward.com/help/firewall-to-internet.jpg">

# Getting Started 
- Fortigate Firewall Multi-Url importer is a URL list importing tool for fortigate firewall devices  </br>
- Tested on <b>Fortigate 600D , firmware 5.2 </b>and works fine ! </br>

# Prerequisites

- You need to install at least .Net Framework 4. 0 

# Installing 
- Download FortiSetup folder then start installing by clicking setup.exe  

# How to Use 
 - After installing program , run it and select your object option as FQND or Subnet. click "Veri Al "  button to read data from txt file </br>
- Then click "URL Script" button or "Subnet Script" button  to create script for url/subnet list in order to import Fortigate device. then click "adress group " button to create adress group script for url/subnet list. 
- Then select scprit output or address group output then click "script kaydet" button to save your script as txt file .
- You must import your script output to fortigate as first. Then import address group script to fortigate.
 *-* Türkçe *-* 
 - Programı kurduktan sonra, nesne  türümüzü seçiyoruz. Url listesi için FQDN , ip listesi için subnet , sonra "Veri AL" butonuna basarak , ip/url listesinin olduğu txt dosyasını seçiyoruz </br> 
 - Subnet script oluşturmak için Subnet Script, url sicript oluşturma için Url Script butonuna basıyuroz. </br>
 - Program otomatik olarak ip/url sağındaki ve solundaki boşlukıarı siler. </br>
 - <b>"Script kaydet"</b> butonu ile url/subnet scriptini ve addres Grup script çıktılarını txt olarak kaydediyoruz </br>
 - Artık scriptlerimiz fortigate cihazımıza import için hazır. İlk önce url/subnet script çıktısını, ardından ilgili adres grup çıktısını import etmeyi unutmayınız 
# Authors 

<a href="https://github.com/farcompen"> Faruk GÜNGÖR </a>

# License
This project is licensed under the MIT License - see the <a href="https://github.com/farcompen/Fortimporter/blob/master/LICENSE">LICENSE file </a> for details



