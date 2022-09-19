function selectHandler(e) {
    onselectstart = e.preventDefault();        
}

function stopPrntScr() {
    var inpFld = document.createElement("input");
    inpFld.setAttribute("value", ".");
    inpFld.setAttribute("width", "0");
    inpFld.style.height = "0px";
    inpFld.style.width = "0px";
    inpFld.style.border = "0px";
    document.body.appendChild(inpFld);
    inpFld.select();
    document.execCommand("copy");
    inpFld.remove(inpFld);
}

function AccessClipboardData() {
    try {
        window.clipboardData.setData('text', "Access Restricted");
    } catch (err) {
    }
}

function keyupHandler(e){
    var keyCode = e.keyCode ? e.keyCode : e.which;
    if (keyCode == 44) {
        stopPrntScr();
    }
}


document.addEventListener('selectstart', selectHandler);
document.addEventListener("keyup", keyupHandler);
setInterval("AccessClipboardData()", 300);   

function auth(){
    var password = "U2FsdGVkX1+Xa8/Rwga6AYAPv1xMoPGkL9bdI/5Gvu4=";
    var input = prompt("Enter password to turn off protected mode",);
    if (CryptoJS.AES.decrypt(password, "p4ssw0rdp4ssw0rd").toString(CryptoJS.enc.Utf8) == input){
        document.removeEventListener('keyup', keyupHandler);
        document.removeEventListener('selectstart', selectHandler);
        alert("Turned off");
    } else {
        alert("Don't try to change things that don't belong to you!");
    }
}



