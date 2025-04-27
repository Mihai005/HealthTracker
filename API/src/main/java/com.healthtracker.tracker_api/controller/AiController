package com.healthtracker.tracker_api.controller;

import org.springframework.http.*;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.client.RestTemplate;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

@RestController
@RequestMapping("/api/ai")
public class AiController {
    private final RestTemplate restTemplate = new RestTemplate();
    private final String OPENAI_API_KEY = // Insert an API key here, for DeepSeek V3.0

    @PostMapping("/ask")
    public ResponseEntity<String> askAi(@RequestBody Map<String, String> body) {
        String userInput = body.get("prompt");

        HttpHeaders headers = new HttpHeaders();
        headers.setContentType(MediaType.APPLICATION_JSON);
        headers.setBearerAuth(OPENAI_API_KEY);
        headers.add("HTTP-Referer", "https://localhost:8080"); // Replace with your app URL
        headers.add("X-Title", "SmartCoach");

        Map<String, Object> requestBody = new HashMap<>();
        requestBody.put("model", "deepseek/deepseek-chat-v3-0324:free"); // THIS is the DeepSeek model inside OpenRouter
        requestBody.put("messages", List.of(
                Map.of("role", "system", "content", "You are a friendly and expert fitness coach. Answer shortly and clearly."),
                Map.of("role", "user", "content", userInput)
        ));

        HttpEntity<Map<String, Object>> request = new HttpEntity<>(requestBody, headers);

        ResponseEntity<Map> response = restTemplate.postForEntity(
                "https://openrouter.ai/api/v1/chat/completions",
                request,
                Map.class
        );

        if (response.getStatusCode().is2xxSuccessful() && response.getBody() != null) {
            List<Map<String, Object>> choices = (List<Map<String, Object>>) response.getBody().get("choices");
            if (choices != null && !choices.isEmpty()) {
                Map<String, Object> message = (Map<String, Object>) choices.get(0).get("message");
                if (message != null) {
                    String content = (String) message.get("content");
                    return ResponseEntity.ok(content.trim());
                }
            }
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR)
                    .body("No response choices received.");
        } else {
            return ResponseEntity.status(HttpStatus.SERVICE_UNAVAILABLE)
                    .body("AI service not available. Status: " + response.getStatusCode());
        }
    }
}
