package com.healthtracker.tracker_api.controller;

import com.healthtracker.tracker_api.model.LogInModel;
import com.healthtracker.tracker_api.model.SignUpModel;
import com.healthtracker.tracker_api.table.User;
import com.healthtracker.tracker_api.service.UserService;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/auth")
public class AuthController {
    private final UserService userService;

    public AuthController(UserService userService) {
        this.userService = userService;
    }

    @PostMapping("/login")
    public ResponseEntity<?> login(@RequestBody LogInModel request) {
        if (this.userService.login(request.getUsername(), request.getPassword()))
            return ResponseEntity.ok("Login successful");
        return ResponseEntity.status(HttpStatus.UNAUTHORIZED).body("Invalid credentials");
    }

    @PostMapping("/signup")
    public ResponseEntity<?> signup(@RequestBody SignUpModel request) {
        if (!request.getPassword().equals(request.getRepeatPassword()))
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body("Passwords don't match!");
        try {
            User user = this.userService.createUser(new User(request.getUsername(), request.getPassword()));
            return ResponseEntity.ok(user);
        }
        catch (Exception ex) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(ex.getMessage());
        }
    }
}
