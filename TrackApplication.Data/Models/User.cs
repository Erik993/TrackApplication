using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text;

namespace TrackApplicationData.Models;

public abstract class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    [MaxLength(100)]
    public string UserName { get; set; } = string.Empty;

    //setter calls method to check if email has valid structure
    [Required]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public bool IsActive { get; set; }

    public User() { }

    //email is validated in contructor via SetEmail method
    public User(string name, string email, bool isactive)
    {
        UserName = name;
        Email = email;
        IsActive = isactive;
    } 
}


    //move logic to viewmodel
    /*
    public void SetEmail(string input)
    {
        if (!IsValidEmail(input))
            throw new ArgumentException($"{input} is not valid email.");
        Email = input;
    }


    private bool IsValidEmail(string input)
    {
        try
        {
            var addr = new MailAddress(input);
            return addr.Address == input;
        }
        catch
        {
            return false;
        }

    }


    public override string ToString()
    {
        return $"ID: {UserId}, Name: {UserName}, Email: {Email}, is Active: {IsActive}";
    }
    */


