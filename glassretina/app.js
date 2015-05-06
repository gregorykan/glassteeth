$(document).ready(function ()
{
	var map;
	var negativeLocations = [];
	var positiveLocations = [];
	var neutralLocations = [];
	var negativePointArray;
	var positivePointArray;
	var neutralPointArray;
	var neutralHeatMap;
	var positiveHeatMap;
	var negativeHeatMap;
	var geocoder = new google.maps.Geocoder();
	var pusher = new Pusher('45c06fa98717fe603c5a');
	var channel = pusher.subscribe('tweetStream');
	var myLatlng;

	$('.submit-button').click(function (e)
		{
			e.preventDefault();
			var input = $('.myForm').find('input:text').val();
			console.log(input);
			toggleLiveSearch(input);
		});

	channel.bind('tweetEvent', function (data) 
	{
		console.log("tweet with coordinates: " + data["message"]["Body"]);
		fillMap(data["message"]["Latitude"], data["message"]["Longitude"], data["message"]["Sentiment"]);
	});

	channel.bind('tweetEventWithPlace', function (data)
	{
		console.log("tweet with location: "+ data["message"]["Body"]);
		geocode(data["message"]["Location"], data["message"]["Sentiment"]);
	});

	initialize();

	function toggleLiveSearch(input)
	{
		console.log("live search toggled for term: " + input);
		$.ajax(
		{
			url:"http://localhost:49394/api/stream?input=" + input,
			method: "GET",
			dataType: "jsonp"
		}).done(function(data)
		{

		});
	}

	function geocode(place, sentiment)
	{
		$.ajax(
		{
			url: "https://maps.googleapis.com/maps/api/geocode/json?address=" + place,
			method: "GET",
		}).done(function (data)
		{
			var jsonReturn = data["results"];
			var lat = jsonReturn[0]["geometry"]["location"]["lat"];
			var lng = jsonReturn[0]["geometry"]["location"]["lng"];
		fillMap(lat, lng, sentiment);
		});
	}

	function fillMap(lat, lng, sentiment)
	{
		if (sentiment === "positive")
		{
			positiveLocations.push(new google.maps.LatLng(lat, lng));
			positivePointArray = new google.maps.MVCArray(positiveLocations);
			positiveHeatMap.setData(positivePointArray);
		}
		else if (sentiment === "negative")
		{
			negativeLocations.push(new google.maps.LatLng(lat, lng));
			negativePointArray = new google.maps.MVCArray(negativeLocations);
			negativeHeatMap.setData(negativePointArray);
		}
		else if (sentiment === "neutral")
		{
			neutralLocations.push(new google.maps.LatLng(lat, lng));
			neutralPointArray = new google.maps.MVCArray(neutralLocations);
			neutralHeatMap.setData(neutralPointArray);
		}
	}

	function initialize ()
	{
		var mapOptions = 
		{
			zoom: 2,
			center: new google.maps.LatLng(37.774546, -122.433523),
			mapTypeId: google.maps.MapTypeId.SATELLITE
		};

		map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);

		positiveHeatMap = new google.maps.visualization.HeatmapLayer(
		{
			gradient: ['rgba(229,245,224,0)', 'rgba(161,217,155,0.9)', 'rgba(49,163,84,0.9)'],
			radius: 15,
			opacity: 1
		});
		positiveHeatMap.setMap(map);		

		negativeHeatMap = new google.maps.visualization.HeatmapLayer(
		{
			gradient: ['rgba(254,224,210,0)', 'rgba(252,146,114,0.9)', 'rgba(222,45,38,0.9)'],
			radius: 15,
			opacity: 1
		});
		negativeHeatMap.setMap(map);

		neutralHeatMap = new google.maps.visualization.HeatmapLayer(
		{
			gradient: ['rgba(222,235,247,0)', 'rgba(158,202,225,0.9)', 'rgba(49,130,189,0.9)'],
			radius: 15,
			opacity: 1
		});
		neutralHeatMap.setMap(map);

	}
});