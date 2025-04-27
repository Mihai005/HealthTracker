package com.healthtracker.tracker_api.repository;

import com.healthtracker.tracker_api.table.Meal;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface MealRepository extends JpaRepository<Meal, Integer> {
}
