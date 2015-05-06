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

	channel.bind('tweetEvent', function (data) 
	{
		fillMap(data["message"]["Latitude"], data["message"]["Longitude"], data["message"]["Sentiment"]);
	});

	channel.bind('tweetEventWithPlace', function (data)
	{
		geocode(data["message"]["Place"], data["message"]["Sentiment"]);
	});

	initialize();

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
			zoom: 3,
			center: new google.maps.LatLng(37.774546, -122.433523),
			mapTypeId: google.maps.MapTypeId.SATELLITE
		};

		map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);

		positiveHeatMap = new google.maps.visualization.HeatmapLayer(
		{
			gradient: ['rgba(255,255,255,0)', 'rgba(0,255,0,0.9)'],
			radius: 15,
			opacity: 1
		});
		positiveHeatMap.setMap(map);		

		negativeHeatMap = new google.maps.visualization.HeatmapLayer(
		{
			gradient: ['rgba(255,255,255,0)', 'rgba(255,0,0,0.9)'],
			radius: 15,
			opacity: 1
		});
		negativeHeatMap.setMap(map);

		neutralHeatMap = new google.maps.visualization.HeatmapLayer(
		{
			gradient: ['rgba(255,255,255,0)', 'rgba(0,0,255,0.9)'],
			radius: 15,
			opacity: 1
		});
		neutralHeatMap.setMap(map);

	}
});