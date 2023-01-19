	$('.js-example-responsive').select2({
		placeholder: "Available",
		allowClear: true
	});
	
	var go;
	$("body").on("change", "#ddlOccupant", function () {
		
					var floorAndSlot = this.getAttribute("name");
					const floorAndSlotArr = floorAndSlot.split("|");
					var slot = floorAndSlotArr[0];
					var floor = floorAndSlotArr[1];
					var vehicletype = floorAndSlotArr[2];

					$("#hfSelectedOccupant").val($(this).find("option:selected").text());
					$("#hfSlotNumber").val(slot);
					$("#hfVehicleType").val(vehicletype);
					var elements = document.getElementsByName('thisfloor');
					var id = elements[0].getAttribute('id');
					$("#hfFloor").val(floor);

					go = setTimeout(showPage);

		
	
	});



	$("body").on("click", "#ddlFreeUp", function () {

		swal({
		  title: "Are you sure?",
		  text: "you want to free up this parking space?",
		  icon: "warning",
		  buttons: true,
		  dangerMode: true,
		})
		.then((willDelete) => {
		  if (willDelete) {
			  var floorAndSlot = this.getAttribute("name");
				const floorAndSlotArr = floorAndSlot.split("|");
				var slot = floorAndSlotArr[0];
				var floor = floorAndSlotArr[1];
				var vehicletype = floorAndSlotArr[2];

				$("#hfSelectedOccupantFreeUp").val($(this).value);
				$("#hfSlotNumberFreeUp").val(slot);
				$("#hfVehicleTypeFreeUp").val(vehicletype);
				$("#hfFloorFreeUp").val(floor);
					go = setTimeout(freeUpPage);
		  } else {
			swal("Changes not saved!");
		  }
		});
	});


	function showPage() {
		
		swal({
				title: "Occupying this slot...",
				text:" ",
                icon: "loaderimage.gif",
                buttons: false,      
                closeOnClickOutside: false,
                timer: 90000,
                //icon: "success"
            });

		document.forms["Form1"].submit();
	}
	function freeUpPage() {
		swal({
				title: "Freeing up this slot..",
				text:" ",
                icon: "loaderimage.gif",
                buttons: false,      
                closeOnClickOutside: false,
                timer: 90000,
                //icon: "success"
            });

		document.forms["Form2"].submit();
	}

