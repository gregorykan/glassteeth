var glassLungApp = angular.module('glassLungApp', ['pusher-angular']);

glassLungApp.controller('formCtrl', ['$scope', '$http', '$pusher', function($scope, $http, $pusher) 
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
	var client = new Pusher('45c06fa98717fe603c5a');
	var pusher = $pusher(client);

	$scope.sendQuery = function(queryString)
	{
		$http( 
		{
			method: "GET",
			url: "http://localhost:49394/api/stream?input=" + queryString,
		}).success(function(data) 
		{
			$scope.startListening(data);
		});
	}

	$scope.startListening = function (streamId)
	{
		var slicedId = streamId.slice(1,streamId.length-1);
		var stream = pusher.subscribe(slicedId);
		$scope.initialise();

		stream.bind('tweetEvent', function (data) 
		{
			console.log("tweet with coordinates: " + data["message"]["Body"]);
			$scope.fillMap(data["message"]["Latitude"], data["message"]["Longitude"], data["message"]["Sentiment"]);
			$scope.tweetBody = data["message"]["Body"];
		});

		stream.bind('tweetEventWithPlace', function (data)
		{
			console.log("tweet with location: "+ data["message"]["Body"]);
			$scope.geocode(data["message"]["Location"], data["message"]["Sentiment"]);
			$scope.tweetBody = data["message"]["Body"];			
		});
	}

	$scope.geocode = function (place, sentiment)
	{
		$http({
			method:"GET",
			url: "https://maps.googleapis.com/maps/api/geocode/json?address=" + place,
		}).success(function(data)
		{
			var lat = data["results"][0]["geometry"]["location"]["lat"];
			var lng = data["results"][0]["geometry"]["location"]["lng"];
			$scope.fillMap(lat, lng, sentiment);
		});
	}

	$scope.fillMap = function (lat, lng, sentiment)
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

	$scope.initialise = function ()
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

}]);