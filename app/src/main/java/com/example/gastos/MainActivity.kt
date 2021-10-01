package com.example.gastos

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.provider.ContactsContract
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import org.w3c.dom.Text

class MainActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        supportActionBar?.setDisplayShowTitleEnabled(false)

        var loginBtn = findViewById<Button>(R.id.loginBtn)

        loginBtn.setOnClickListener {
            Login()
        }
    }

    fun Login(){
        var email = findViewById<EditText>(R.id.emailTxt).text.toString()
        var password = findViewById<EditText>(R.id.passwordTxt).text.toString()

        if (email.isNullOrEmpty())
            Toast.makeText(this@MainActivity, "Email obrigatório.", Toast.LENGTH_SHORT).show()

        if (password.isNullOrEmpty())
            Toast.makeText(this@MainActivity, "Senha obrigatória.", Toast.LENGTH_SHORT).show()


    }
}