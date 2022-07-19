The best way to store this json data format depends on:
the context of how the data will be used
whether people or code are consuming the json and whether your programming language or library easily supports specific formats.

I suggest the present json structure(unix timestamps) be combined with those with either user timezones or a single separately
stored "render timezone" (json string) depending on the use-case. This allows the most relevant date/time rendering to be
presented to the user and allows you to easily use different date/time formats depending on the user's location/locale.