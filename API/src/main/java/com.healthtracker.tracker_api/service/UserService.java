package com.healthtracker.tracker_api.service;

import com.healthtracker.tracker_api.repository.UserRepository;
import com.healthtracker.tracker_api.table.User;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
public class UserService {
    private final UserRepository userRepository;

    public UserService(UserRepository userRepository) {
        this.userRepository = userRepository;
    }

    public List<User> getAllUsers() {
        return this.userRepository.findAll();
    }

    public boolean login(String username, String password) {
        Optional<User> found = this.userRepository
                .findByUserNameAndPassword(username, password);
        return found.isPresent();
    }

    public User createUser(User user) throws Exception {
        if (this.getAllUsers().stream().map(User::getUserName).anyMatch(name -> name.equals(user.getUserName())))
            throw new Exception("Username already exists!");
        return this.userRepository.save(user);
    }
}
