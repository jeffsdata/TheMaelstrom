//https://bost.ocks.org/mike/bubble-map/

var width = 1000,
	height = 600;

var circleScale = 1000000000;

var path = d3.geo.path()
	.projection(null) //albersUsa projection stated in Make file when creating the topojson data

var radius = d3.scale.sqrt()
	.domain([0, 3e6]) // INPUT domain-range of possible input data values
	//To avoid distortion, make sure that the minimum "domain" and "range" values are both 0
	.range([0, 60]); // OUTPUT range of possible output values

var arc = d3.svg.arc()
	.outerRadius(radius)

var svg = d3.select('#graph').append('svg')
	.attr('height', height)
	.attr('width', width);


var toolTip = d3.select('#graph')
	.append('div')
	.style('position', 'absolute')
	.style('padding', '5px 10px')
	.style('background', '#fff')
    .style('color', '#222')
	.style('opacity', 0)
  .style('font-family', 'Open Sans')
	.style('z-index', 1000);


d3.json('../js/unitedstates.json', function (error, usa) {
    if (error) return console.log(error);

    //build the main land area
    svg.append('path')
		.datum(topojson.feature(usa, usa.objects.nation))
		.attr('class', 'land')
		.attr('d', path);


    svg.selectAll('.states')
        // retrieve the features so that id is accessed
        .data(topojson.feature(usa, usa.objects.counties).features)
        .enter().append('path')
        .attr('id', function(d) { return d.id; })
        .attr("class", "states states-hover")
        .attr('d', path)

        /* County Tooltip /*
         //add Tool Tip
    .on('mouseover', function (d, i) {
        toolTip.transition()
          .style('opacity', .9)
          .style('left', (d3.event.pageX) + 'px')
          .style('top', (d3.event.pageY) + 'px')
        tempColor = this.style.fill; //store current color

        if (d.properties.name != null || d.properties.name != undefined) {
            toolTip.html(d.properties.name + ", " + d.properties.population)
        } else {
            toolTip.html("")
        }
    })
		.on('mouseout', function () {
		    d3.select(this)
			 .transition().delay(400).duration(800)
			 .style('opacity', 1)
			 .style('fill', tempColor)
		})

        */

    //build internal state lines
    svg.append('path')
		.datum(topojson.mesh(usa, usa.objects.states, function (a, b) {
		    return a !== b;
		})) //topojson.mesh
		.attr('class', 'border-states')
		.attr('d', path);


    //show data as layered bubbles
    svg.append("g")
        .attr("class", "bubble")
        .selectAll("circle")
        .data(topojson.feature(usa, usa.objects.counties).features
            .sort(function(a, b) { //sort population low to high
                return b.properties.population - a.properties.population;
            }) //sort
        ) //data
        .enter().append("circle")
        .on('mouseover', function(d, i) {
            d3.select(this).attr('class', 'hover');
            toolTip.transition()
                .style('opacity', .9)
                .style('left', (d3.event.pageX) + 'px')
                .style('top', (d3.event.pageY) + 'px')
            tempColor = this.style.fill; //store current color

            if (d.properties.name != null || d.properties.name != undefined) {
                toolTip.html(d.properties.name + ", " + d.properties.population)
            } else {
                toolTip.html("")
            }
        })
        .on('mouseout', function(d, i) {
            d3.select(this).attr('class', '');

            d3.select(this)
                .transition().delay(400).duration(800)
                .style('opacity', 1)
                .style('fill', tempColor);
        })
        .attr("transform", function(d) {
            return "translate(" + path.centroid(d) + ")"; //Computes the projected centroid
        })
        .attr("r", function(d) {
            return radius(Math.sqrt((d.properties.population * circleScale) * Math.PI)); //radius var with input (domain) and output (range)
        });

}) //d3.json