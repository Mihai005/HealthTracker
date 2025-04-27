package com.healthtracker.tracker_api.table;

import jakarta.persistence.*;
import lombok.Getter;
import lombok.Setter;

import java.util.ArrayList;
import java.util.List;

@Getter
@Entity
@Table(name = "Users", schema = "dbo")
public class User {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "userId")
    private int userId;

    @Setter
    @Column(nullable = false, name = "userName")
    private String userName;

    @Setter
    @Column(nullable = false, name = "userPassword")
    private String password;

    @OneToOne(mappedBy = "user", cascade = CascadeType.ALL, orphanRemoval = true)
    private Goal goal;

    @OneToMany(mappedBy = "user", cascade = CascadeType.ALL, orphanRemoval = true)
    private List<DailyLog> logs = new ArrayList<>();

    public User(String user, String pass)
    {
        this.userName = user;
        this.password = pass;
    }

    public User() {

    }

    @Override
    public String toString()
    {
        return this.userName;
    }
}
