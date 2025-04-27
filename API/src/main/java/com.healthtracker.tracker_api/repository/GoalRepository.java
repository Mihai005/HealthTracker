package com.healthtracker.tracker_api.repository;

import com.healthtracker.tracker_api.table.Goal;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.Optional;

@Repository
public interface GoalRepository extends JpaRepository<Goal, Integer> {
    Optional<Goal> findByUserId(int userId);
}
