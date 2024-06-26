

var isVisible = false;
function showHide(id) {
    if (isVisible) {
        var trGroup = document.getElementsByClassName(id);
        for (var i = 0; i < trGroup.length;i++) {
            trGroup[i].style.display = "none";
        }
        isVisible = false;
        console.log("Hide-Ok");
       
    }
    else {       
        var trGroup = document.getElementsByClassName(id);
        for (var i = 0; i < trGroup.length; i++) {
            trGroup[i].style.display = "block";
        }
        isVisible = true;
        console.log("Show-Ok");
    }
}