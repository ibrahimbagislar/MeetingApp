
document.getElementById("addPossibleDateButton").addEventListener("click", function () {
    var container = document.getElementById("possibleDatesContainer");

    var newDiv = document.createElement("div");
    newDiv.className = "mb-3";

    var label = document.createElement("label");
    label.innerText = "Yeni Olası Toplantı Tarihi: ";
    label.style.display = "inline-block";
    newDiv.appendChild(label);

    var trashIcon = document.createElement("i");
    trashIcon.className = "fa fa-trash";
    trashIcon.style.cursor = "pointer";
    trashIcon.addEventListener("click", function () {
        container.removeChild(newDiv); // tıklanan divi sil
    });
    newDiv.appendChild(trashIcon);

    var input = document.createElement("input");
    input.type = "datetime-local";
    input.className = "form-control";
    input.name = "PossibleDates";
    input.style.display = "inline-block"; 
    newDiv.appendChild(input);

    container.appendChild(newDiv);
});
