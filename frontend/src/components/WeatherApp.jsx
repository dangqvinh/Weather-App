import React, { useState } from "react";
import MapSelector from "./MapSelector";

export const WeatherApp = () => {
  const [position, setPosition] = useState({ lat: 21.0285, lng: 105.8542 });
  const [weather, setWeather] = useState(null);

  const fetchWeather = async (lat, lon) => {
    try {
      const res = await fetch(`http://localhost:5036/api/weather?lat=${lat}&lon=${lon}`);
      const data = await res.json();
      setWeather(data);
    } catch (err) {
      console.error(err);
    }
  };

  const handleSelectPosition = (lat, lon) => {
    setPosition({ lat, lng: lon });
    fetchWeather(lat, lon);
  };

  return (
    <div style={{ height: "100vh", width: "100vw", position: "relative" }}>
      <MapSelector onSelect={handleSelectPosition} />

      {/* Overlay thông tin weather */}
      <div style={{
        position: "absolute",
        top: 20,
        left: 20,
        backgroundColor: "rgba(0, 0, 0, 0.9)",
        padding: "12px 16px",
        borderRadius: "12px",
        boxShadow: "0 4px 12px rgba(0,0,0,0.2)",
        maxWidth: "250px"
      }}>
        <h2 className="font-bold text-lg mb-2">Vị trí hiện tại</h2>
        <p>{position.lat.toFixed(4)}, {position.lng.toFixed(4)}</p>

        {weather && weather.current_weather && (
          <>
            <p>Nhiệt độ: {weather.current_weather.temperature}°C</p>
            <p>Gió: {weather.current_weather.windspeed} km/h</p>
            <p>Hướng gió: {weather.current_weather.winddirection}°</p>
          </>
        )}
      </div>
    </div>
  );
};
