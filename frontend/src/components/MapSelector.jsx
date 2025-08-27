import React, { useEffect, useRef, useState } from "react";
import trackasiagl from "trackasia-gl";
import "trackasia-gl/dist/trackasia-gl.css";

const MapSelector = ({ onSelect }) => {
  const mapContainer = useRef(null);
  const mapRef = useRef(null);
  const markerRef = useRef(null);

  const [position, setPosition] = useState({ lat: 21.0285, lng: 105.8542 });

  useEffect(() => {
    const initializeMap = async () => {
      if (mapRef.current) return;

      try {
        const styleJson = await fetch("http://localhost:5036/api/map-style").then(res => res.json());

        mapRef.current = new trackasiagl.Map({
          container: mapContainer.current,
          style: styleJson,
          center: [position.lng, position.lat],
          zoom: 12,
        });

        // Thêm marker
        markerRef.current = new trackasiagl.Marker()
          .setLngLat([position.lng, position.lat])
          .addTo(mapRef.current);

        mapRef.current.on("click", (e) => {
          const { lng, lat } = e.lngLat;
          setPosition({ lat, lng });
          markerRef.current.setLngLat([lng, lat]);
          if (onSelect) onSelect(lat, lng);
        });

        mapRef.current.on("styleimagemissing", (e) => {
          console.warn("Missing image in style:", e.id);
        });

      } catch (err) {
        console.error("Failed to initialize map:", err);
      }
    };

    initializeMap();

    return () => {
      if (mapRef.current) {
        mapRef.current.remove();
        mapRef.current = null;
      }
    };
  }, [onSelect]);

  return (
    <div
      ref={mapContainer}
      style={{ position: "absolute", top: 0, left: 0, right: 0, bottom: 0 }}
    />
  );
};

export default MapSelector;
