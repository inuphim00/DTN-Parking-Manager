var go;

//register user
var modal = document.getElementById("myModal");
var btn = document.getElementById("myBtn");
var span = document.getElementsByClassName("close")[0];


btn.onclick = function () {
    modal.style.display = "block";
}
span.onclick = function () {
    modal.style.display = "none";
}
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}
//end of register user

//edit user
var modalEdit = document.getElementById("myModalEdit");
var btnEdit = document.getElementById("editUser");
var spanEdit = document.getElementsByClassName("closeEdit")[0];
$("body").on("click", "#editUser", function () {

    var details = this.getAttribute("name");

    const detailsArr = details.split("|");
    var fullNameEdit = detailsArr[0];
    var contactNumberEdit = detailsArr[1];
    var plateNumberEdit = detailsArr[2];
    var vehicleTypeEdit = detailsArr[3];
    $("#hfPlateNumberOriginal").val(plateNumberEdit);
    $("#hfFullNameOriginal").val(fullNameEdit);
    $("#fullNameEdit").val(fullNameEdit);
    $("#plateNumberEdit").val(plateNumberEdit);
    $("#contactNumberEdit").val(contactNumberEdit);
    $("#vehicleTypeEdit").val(vehicleTypeEdit);
    modalEdit.style.display = "block";


    const platenumber = document.getElementById('plateNumberEdit');
    const platenumberText = document.getElementById('plateNumberEditText');
    if (vehicleTypeEdit == "Bicycle") {
        platenumber.style.display = 'none';
        platenumberText.style.display = 'none';
    } else {
        platenumber.style.display = 'block';
        platenumberText.style.display = 'block';
    }
});
spanEdit.onclick = function () {
    modalEdit.style.display = "none";
}
window.onclick = function (event) {
    if (event.target == modal) {
        modalEdit.style.display = "none";
    }
}
//end of edit user


//register user function 
$("body").on("click", "#ddlSubmit", function () {
    var fullname = $("#fullName").val();

    let occupants = document.querySelectorAll('h6.card-title');

     occupants.forEach(occupants => 
     {
            var vehicle = $("#vehicleType").val();
            var platenumber = $("#plateNumber").val();
            var contactnumber = $("#contactNumber").val();
            if (fullname && contactnumber) {
                if (vehicle == "Bicycle" && platenumber == "" || vehicle != "Bicycle" && platenumber != "") {
                
                    if (occupants.textContent.includes(fullname)) {
                        swal("User already exist!", " A vehicle instance of this user already exist, try a different name", "error");
                    } else {
                    
                        var validPhone = /^\d{11}$/;
                        if (contactnumber.match(validPhone)) {
                            swal({
                                title: "Register a " + vehicle,
                                text: "under the name of " + fullname,
                                icon: "warning",
                                buttons: true,
                                dangerMode: true,
                            })
                                .then((willDelete) => {
                                    if (willDelete) {

                                        $("#hfFullName").val(fullname);
                                        $("#hfContactNumber").val(contactnumber);
                                        $("#hfVehicleType").val(vehicle);
                                        $("#hfPlateNumber").val(platenumber);
                                        go = setTimeout(register);

                                    } else {
                                        swal("Changes not saved!");
                                    }
                                });
                        } else {
                            swal("Input valid phone number", " ", "error");
                        }
                        }
               



                } else {
                    swal("Please fill all forms", " ", "error");
                }

            } else {
                swal("Please fill all forms", " ", "error");
            }
      });


   


});
//end of register user function
//edit user function
$("body").on("click", "#ddlSubmitEdit", function () {
    var fullname = $("#fullNameEdit").val();
    var vehicle = $("#vehicleTypeEdit").val();
    var platenumber = $("#plateNumberEdit").val();
    var contactnumber = $("#contactNumberEdit").val();
    var orig = $("#hfFullNameOriginal").val();


    if (fullname && contactnumber) {
        if (vehicle == "Bicycle" && platenumber == "" || vehicle != "Bicycle" && platenumber != "") {
            var validPhone = /^\d{11}$/;
            if (contactnumber.match(validPhone)) {
                swal({
                    title: "Edit this Occupant?",
                    text: " ",
                    icon: "warning",
                    buttons: true,
                    dangerMode: true,
                })
                    .then((willDelete) => {
                        if (willDelete) {

                            $("#hfFullNameEdit").val(fullname);
                            $("#hfContactNumberEdit").val(contactnumber);
                            $("#hfVehicleTypeEdit").val(vehicle);
                            $("#hfPlateNumberEdit").val(platenumber);


                            go = setTimeout(editUser);

                        } else {
                            swal("Changes not saved!");
                        }
                    });
            } else {
                swal("Input valid phone number", " ", "error");
            }
        } else {
            swal("Please fill all forms", " ", "error");
        }


    } else {
        swal("Please fill all forms", " ", "error");
    }


});

$("body").on("click", "#ddlDeleteUser", function () {
    var userToDelete = this.getAttribute("name");
    swal({
        title: "Delete " + userToDelete + "?",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {

                $("#hfUserToDelete").val(userToDelete);

                go = setTimeout(deleteUser);

            } else {
                swal("Changes not saved!");
            }
        });
});

function deleteUser() {

    swal({
        title: "Deleting user...",
        text: " ",
        icon: "https://cdn.dribbble.com/users/2245289/screenshots/6835230/dribbble.gif",
        buttons: false,
        closeOnClickOutside: false,
        timer: 90000,
        //icon: "success"
    });

    document.forms["Form4"].submit();
}



function register() {

    swal({
        title: "Registering new Vehicle...",
        text: " ",
        icon: "https://cdn.dribbble.com/users/2245289/screenshots/6835230/dribbble.gif",
        buttons: false,
        closeOnClickOutside: false,
        timer: 90000,
        //icon: "success"
    });

    document.forms["Form3"].submit();
}

function editUser() {

    swal({
        title: "Updating Occupant...",
        text: " ",
        icon: "https://cdn.dribbble.com/users/2245289/screenshots/6835230/dribbble.gif",
        buttons: false,
        closeOnClickOutside: false,
        timer: 90000,
        //icon: "success"
    });

    document.forms["Form5"].submit();
}

const searchBox = document.getElementById('searchBox');

searchBox.addEventListener('keyup', e => {
    let currentValue = e.target.value.toLowerCase();
    let occupants = document.querySelectorAll('h6.card-title');
    occupants.forEach(occupants => {
        if (occupants.textContent.toLowerCase().includes(currentValue)) {
            occupants.parentNode.parentNode.parentNode.parentNode.style.display = 'block';
        } else {
            occupants.parentNode.parentNode.parentNode.parentNode.style.display = 'none'
        }

    })
});

$("body").on("change", "#vehicleType", function () {


    var selected = $(this).find("option:selected").text();
    const platenumber = document.getElementById('plateNumber');
    if (selected == "Bicycle") {
        platenumber.style.display = 'none';
    } else {
        platenumber.style.display = 'block';
    }

});

$("body").on("change", "#vehicleTypeEdit", function () {


    var selected = $(this).find("option:selected").text();
    const platenumber = document.getElementById('plateNumberEdit');
    const platenumberText = document.getElementById('plateNumberEditText');
    var originalPlateNumber = $("#hfPlateNumberOriginal").val();

    if (selected == "Bicycle") {
        $("#plateNumberEdit").val("");
        platenumber.style.display = 'none';
        platenumberText.style.display = 'none';
    } else {
        $("#plateNumberEdit").val(originalPlateNumber);
        platenumber.style.display = 'block';
        platenumberText.style.display = 'block';
    }

});


