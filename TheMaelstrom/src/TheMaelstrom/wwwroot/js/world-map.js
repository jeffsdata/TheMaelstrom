// Map Script

// Coded by Patrick.Brockmann@lsce.ipsl.fr

$(document).ready(function () {

    var w = parseInt(d3.select("#graph").style('width'));
    var mapRatio = .5;
    var h = w * mapRatio;

    var xy = d3.geo.equirectangular()
              .scale(w);

    var path = d3.geo.path()
        .projection(xy);

    var svg = d3.select("#graph").insert("svg:svg")
        .attr("width", w)
            .attr("height", h);

    var states = svg.append("svg:g")
        .attr("id", "states");

    var circles = svg.append("svg:g")
        .attr("id", "circles");

    var labels = svg.append("svg:g")
        .attr("id", "labels");

    d3.json("js/unitedstates.json", function (collection) {
        states.selectAll("path")
            .data(collection.features)
          .enter().append("svg:path")
            .attr("d", path)
                  .on("mouseover", function (d) {
                      d3.select(this).style("fill", "#1e90ff")
                          .append("svg:title")
                          .text(d.properties.name);
                  })
                  .on("mouseout", function (d) {
                      d3.select(this).style("fill", "#444");
                  })
    });

    var scalefactor = 40.;

    d3.csv("js/world-memberdata.csv", function (csv) {
        circles.selectAll("circle")
            .data(csv)
          .enter()
          .append("svg:circle")
            .attr("cx", function (d, i) { return xy([+d["longitude"], +d["latitude"]])[0]; })
            .attr("cy", function (d, i) { return xy([+d["longitude"], +d["latitude"]])[1]; })
            .attr("r", function (d) { return (Math.sqrt(Math.PI * (+d["2016"] * scalefactor))); })
            .attr("title", function (d) { return d["country"] + ": " + Math.round(d["2016"]); })
                  .on("mouseover", function (d) {
                      d3.select(this).style("fill", "#FC0");
                  })
                  .on("mouseout", function (d) {
                      d3.select(this).style("fill", "#00bfff");
                  });

        labels.selectAll("labels")
            .data(csv)
          .enter()
          .append("svg:text")
              .attr("x", function (d, i) { return xy([+d["longitude"], +d["latitude"]])[0]; })
              .attr("y", function (d, i) { return xy([+d["longitude"], +d["latitude"]])[1]; })
              .attr("dy", "0.3em")
              .attr("text-anchor", "middle")
              .text(function (d) { return Math.round(d["2016"]); });

    });

    function redraw(year) {
        circles.selectAll("circle")
        .transition()
            .duration(1000).ease("linear")
            .attr("r", function (d) { return (+d[year]) * scalefactor; })
            .attr("title", function (d) { return d["country"] + ": " + Math.round(d[year]); });

        labels.selectAll("text")
            .text(function (d) { return Math.round(d[year]); });
    }

});
